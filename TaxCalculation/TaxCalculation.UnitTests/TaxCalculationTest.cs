using Moq;
using Xunit;

using TaxCalculation.Core.Entities.TaxCalculation;
using TaxCalculation.Core.Data;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Tests
{
    public class TaxCalculationInteractorTests
    {
        private readonly Mock<ITaxSettings> _mockTaxSettings;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly TaxCalculationInteractor _taxCalculationInteractor;

        public TaxCalculationInteractorTests()
        {
            _mockTaxSettings = new Mock<ITaxSettings>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockTaxStatistics = new Mock<ITaxStatisticsRepository>();
            _mockUnitOfWork.Setup(uow => uow.TaxStatistics).Returns(mockTaxStatistics.Object);

            _mockTaxSettings.Setup(s => s.BandA).Returns(5000);
            _mockTaxSettings.Setup(s => s.BandB).Returns(20000);
            _mockTaxSettings.Setup(s => s.BasicRate).Returns(0.2m);
            _mockTaxSettings.Setup(s => s.HigherRate).Returns(0.4m);
            _mockTaxSettings.Setup(s => s.AnnualMonthCount).Returns(12);

            _taxCalculationInteractor = new TaxCalculationInteractor(_mockTaxSettings.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetIncomeTax_ShouldReturnCorrectTaxCalculation_WhenGrossAnnualSalaryIsValid()
        {
            int grossAnnualSalary = 40000;

            var result = await _taxCalculationInteractor.GetIncomeTax(grossAnnualSalary);

            Assert.NotNull(result);
            Assert.Equal(3333.33m, result.GrossMonthlySalary, precision: 2);
            Assert.Equal(11000m, result.AnnualTaxPaid, precision: 2);
            Assert.Equal(29000m, result.NetAnnualSalary, precision: 2);
            Assert.Equal(2416.67m, result.NetMonthlySalary, precision: 2);
            Assert.Equal(916.67m, result.MonthlyTaxPaid, precision: 2);

            _mockUnitOfWork.Verify(u => u.TaxStatistics.Create(It.IsAny<TaxStatisticsDto>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetIncomeTax_ShouldReturnZeroTax_WhenGrossAnnualSalaryIsLessThanBandA()
        {
            int grossAnnualSalary = 4000;

            var result = await _taxCalculationInteractor.GetIncomeTax(grossAnnualSalary);

            Assert.NotNull(result);
            Assert.Equal(0m, result.AnnualTaxPaid);
            Assert.Equal(grossAnnualSalary, result.NetAnnualSalary, precision: 2);
            Assert.Equal(grossAnnualSalary / 12m, result.NetMonthlySalary, precision: 2);
            Assert.Equal(0m, result.MonthlyTaxPaid, precision: 2);

            _mockUnitOfWork.Verify(u => u.TaxStatistics.Create(It.IsAny<TaxStatisticsDto>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetIncomeTax_ShouldReturnValidResult_WhenGrossAnnualSalaryIsInBandB()
        {
            int grossAnnualSalary = 15000;

            var result = await _taxCalculationInteractor.GetIncomeTax(grossAnnualSalary);

            Assert.NotNull(result);
            Assert.Equal(1250m, result.GrossMonthlySalary, precision: 2);
            Assert.Equal(2000m, result.AnnualTaxPaid, precision: 2);
            Assert.Equal(13000m, result.NetAnnualSalary, precision: 2);
            Assert.Equal(1083.33m, result.NetMonthlySalary, precision: 2);
            Assert.Equal(166.67m, result.MonthlyTaxPaid, precision: 2);

            _mockUnitOfWork.Verify(u => u.TaxStatistics.Create(It.IsAny<TaxStatisticsDto>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}