using TaxCalculation.Core.Entities.TaxCalculation;

namespace TaxCalculation.API.Settings.Infrastructure
{
    public class TaxSettings:ITaxSettings
    {
        public decimal BandA { get; set; }
        public decimal BandB { get; set; }
        public decimal BasicRate { get; set; }
        public decimal HigherRate { get; set; }
        public int AnnualMonthCount { get; set; }
    }
}