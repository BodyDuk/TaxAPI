using TaxCalculation.API.Settings.Infrastructure;
using TaxCalculation.Core.Data;
using TaxCalculation.Core.Entities.TaxCalculation;
using TaxCalculation.Data.Common;
using TaxCalculation.Data.DataAccess;

namespace TaxCalculation.API.Settings
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITaxStatisticsRepository, TaxStatisticsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddInteractors(this IServiceCollection services)
        {
            services.AddScoped<ITaxCalculationBoundary, TaxCalculationInteractor>();

            return services;
        }

        public static IServiceCollection AddSettings(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSingleton<ITaxSettings>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var taxSettingsSection = configuration.GetSection("TaxSettings") ?? throw new InvalidOperationException("tax_settings_missing");
                var taxSettings = new TaxSettings();
                taxSettingsSection.Bind(taxSettings);
                return taxSettings;
            });

            return services;
        }
    }
}