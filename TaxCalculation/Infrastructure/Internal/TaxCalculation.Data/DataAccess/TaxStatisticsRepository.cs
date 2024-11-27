using TaxCalculation.Core.Entities.TaxCalculation;
using TaxCalculation.Data.Common;

using TaxCalculator.Domain.Entities;

namespace TaxCalculation.Data.DataAccess
{
    public class TaxStatisticsRepository:GenericRepository<TaxStatisticsDto>, ITaxStatisticsRepository
    {
        public TaxStatisticsRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<TaxStatisticsDto> GetAll() => db.TaxStatistics.Select(tx => tx).ToList();
    }
}