using Microsoft.EntityFrameworkCore;

using TaxCalculator.Domain.Entities;

namespace TaxCalculation.Data.Common
{
    public class AppDbContext:DbContext
    {
        public DbSet<TaxStatisticsDto> TaxStatistics { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaxStatisticsDto>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(e => e.AnnualTaxPaid)
                    .HasPrecision(18, 2);

                entity.Property(e => e.GrossMonthlySalary)
                      .HasPrecision(18, 2);

                entity.Property(e => e.MonthlyTaxPaid)
                      .HasPrecision(18, 2);

                entity.Property(e => e.NetAnnualSalary)
                      .HasPrecision(18, 2);

                entity.Property(e => e.NetMonthlySalary)
                      .HasPrecision(18, 2);
            });
        }
    }
}