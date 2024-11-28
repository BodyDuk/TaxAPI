using TaxCalculation.Core.Data;
using TaxCalculation.Core.Entities.TaxCalculation;

namespace TaxCalculation.Data.Common
{
    public class UnitOfWork(
        AppDbContext dbContext,
        ITaxStatisticsRepository taxStatistics):IUnitOfWork
    {
        private bool disposed = false;
        private readonly AppDbContext db = dbContext;
        public ITaxStatisticsRepository TaxStatistics { get; private set; } = taxStatistics;

        public async Task SaveAsync()
            => await db.SaveChangesAsync();
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
                    db.Dispose();

                disposed = true;
            }
        }
    }
}