using System.ComponentModel.DataAnnotations;

namespace TaxCalculation.API.Controllers.TaxCalculation.Models
{
    public class IncomeDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "range_exeption")]
        public int GrossAnnualSalary { get; set; }
    }
}