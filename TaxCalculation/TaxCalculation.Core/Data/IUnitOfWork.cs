using TaxCalculation.Core.Entities.TaxCalculation;

namespace TaxCalculation.Core.Data
{
    public interface IUnitOfWork:IDisposable
    {
        ITaxStatisticsRepository TaxStatistics { get; }
        Task SaveAsync();
    }
}