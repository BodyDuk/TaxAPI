using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

using TaxCalculation.Core.Data;

namespace TaxCalculation.Data.Common
{
    public class GenericRepository<T>:IRepository<T> where T : class
    {
        protected AppDbContext db;
        protected DbSet<T> dbSet;

        public GenericRepository(AppDbContext context)
        {
            this.db = context;
            this.dbSet = context.Set<T>();
        }

        public void Create(T item) => db.Add(item);
        public T? GetById(int id) => db.Find<T>(id);
        public void Update(T item) =>
            db.Entry(item).State = EntityState.Modified;

        public virtual IList<T> Get(Expression<Func<T, bool>>? filter = null,
                  Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                  params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            foreach(Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if(filter != null)
                query = query.Where(filter);

            if(orderBy != null)
                query = orderBy(query);
            return query.ToList();
        }
    }
}