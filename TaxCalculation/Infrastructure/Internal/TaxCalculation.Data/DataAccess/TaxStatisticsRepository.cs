using TaxCalculation.Core.Entities.TaxCalculation;
using TaxCalculation.Data.Common;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Data.DataAccess
{
    public class TaxStatisticsRepository(AppDbContext context):GenericRepository<TaxStatisticsDto>(context), ITaxStatisticsRepository
    {
        public IEnumerable<TaxStatisticsDto> GetAll() => [.. db.TaxStatistics.Select(tx => tx)];
    }
}