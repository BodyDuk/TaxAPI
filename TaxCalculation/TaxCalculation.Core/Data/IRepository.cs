using System.Linq.Expressions;

namespace TaxCalculation.Core.Data
{
    public interface IRepository<T> where T : class
    {
        T? GetById(int id);
        void Create(T item);
        void Update(T item);
        IList<T> Get(Expression<Func<T, bool>>? filter = null,
                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                      params Expression<Func<T, object>>[] includes);
    }
}