using TaxCalculator.Domain.Entities;

namespace TaxCalculation.Core.Entities.TaxCalculation.Models
{
    public class IncomeTaxResult
    {
        public decimal GrossMonthlySalary { get; set; }
        public decimal NetAnnualSalary { get; set; }
        public decimal NetMonthlySalary { get; set; }
        public decimal AnnualTaxPaid { get; set; }
        public decimal MonthlyTaxPaid { get; set; }

        public TaxStatisticsDto ToDomainModel(int grossAnnualSalary) =>
            new()
            {
                GrossAnnualSalary = grossAnnualSalary,
                GrossMonthlySalary = GrossMonthlySalary,
                NetAnnualSalary = NetAnnualSalary,
                NetMonthlySalary = NetMonthlySalary,
                AnnualTaxPaid = AnnualTaxPaid,
                MonthlyTaxPaid = MonthlyTaxPaid
            };
    }
}