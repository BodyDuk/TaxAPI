using TaxCalculation.Core.Data;
using TaxCalculator.Domain.Entities;

namespace TaxCalculation.Core.Entities.TaxCalculation
{
    public interface ITaxStatisticsRepository:IRepository<TaxStatisticsDto>
    {
        IEnumerable<TaxStatisticsDto> GetAll();
    }
}