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

            //_unitOfWork.TaxStatistics.Create(result.ToDomainModel(grossAnnualSalary));
            await _unitOfWork.SaveAsync();

            return result;
        }

        private decimal CalculateMonthly(decimal annualMetric)
            => annualMetric / _settings.AnnualMonthCount;

        private decimal CalculateAnnualTaxPaid(int grossAnnualSalary)
        {
            if(grossAnnualSalary <= _settings.BandA)
                return 0;

            if(grossAnnualSalary <= _settings.BandB)
                return (grossAnnualSalary - _settings.BandA) * _settings.BasicRate;
             else
                return (_settings.BandB - _settings.BandA) * _settings.BasicRate + (grossAnnualSalary - _settings.BandB) * _settings.HigherRate;
        }
    }
}