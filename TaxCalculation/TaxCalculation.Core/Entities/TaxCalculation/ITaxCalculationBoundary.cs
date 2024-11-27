using TaxCalculation.Core.Entities.TaxCalculation.Models;

namespace TaxCalculation.Core.Entities.TaxCalculation
{
    public interface ITaxCalculationBoundary
    {
        Task<IncomeTaxResult> GetIncomeTax(int grossAnnualSalary);
    }
}