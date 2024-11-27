using Microsoft.AspNetCore.Mvc;

using TaxCalculation.API.Controllers.TaxCalculation.Models;
using TaxCalculation.Core.Entities.TaxCalculation;

namespace TaxCalculation.API.Controllers.TaxCalculation
{
    [ApiController]
    [Route("api/tax-calculation")]
    public class TaxCalculationController(ITaxCalculationBoundary taxInteractor):ControllerBase
    {
        [HttpGet("income")]
        public async Task<IActionResult> Get([FromQuery] IncomeDto income)
            => Ok(await taxInteractor.GetIncomeTax(income.GrossAnnualSalary));
    }
}