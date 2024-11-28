using TaxCalculation.Core.Data;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Core.Entities.TaxCalculation
{
    public interface ITaxStatisticsRepository:IRepository<TaxStatisticsDto>
    {
        IEnumerable<TaxStatisticsDto> GetAll();
    }
}