using TaxCalculation.API.Settings;
using TaxCalculation.Data.Common;

using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("connection_string_not_found.");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddRepositories();
        builder.Services.AddInteractors();
        builder.Services.AddSettings(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        using(var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            appDbContext.Database.EnsureCreated();
        }

        app.Run();
    }
}