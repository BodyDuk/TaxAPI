using TaxCalculation.Core.Data;
using TaxCalculation.Core.Entities.TaxCalculation.Models;

namespace TaxCalculation.Core.Entities.TaxCalculation
{
    public class TaxCalculationInteractor(ITaxSettings settings, IUnitOfWork unitOfWork):ITaxCalculationBoundary
    {
        private readonly ITaxSettings _settings = settings;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IncomeTaxResult> GetIncomeTax(int grossAnnualSalary)
        {
            if(grossAnnualSalary < 0)
                return new IncomeTaxResult();

            var result = new IncomeTaxResult
            {
                GrossMonthlySalary = CalculateMonthly(grossAnnualSalary),
                AnnualTaxPaid = CalculateAnnualTaxPaid(grossAnnualSalary)
            };
            result.NetAnnualSalary = grossAnnualSalary - result.AnnualTaxPaid;
            result.NetMonthlySalary = CalculateMonthly(result.NetAnnualSalary);
            result.MonthlyTaxPaid = CalculateMonthly(result.AnnualTaxPaid);

            _unitOfWork.TaxStatistics.Create(result.ToDomainModel(grossAnnualSalary));
            await _unitOfWork.SaveAsync();

            return result;
        }

        private decimal CalculateMonthly(decimal annualMetric)
            => annualMetric / _settings.AnnualMonthCount;

        private decimal CalculateAnnualTaxPaid(int grossAnnualSalary)
        {
            decimal tax = Math.Max(0, Math.Min(grossAnnualSalary, _settings.BandB) - _settings.BandA) * _settings.BasicRate;
            tax += Math.Max(0, grossAnnualSalary - _settings.BandB) * _settings.HigherRate;
            return tax;
        }
    }
}