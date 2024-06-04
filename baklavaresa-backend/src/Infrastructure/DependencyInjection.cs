using Application.Services;
using Domain.Repositories;
using Infrastructure.Data.Persistence;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void InfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IClockService, ClockService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddRepositories();
    }
    public static void SetupDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IDatabase, DatabaseContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
    }

    public static void SetupInMemoryDatabase(this IServiceCollection services, string inMemoryDbName)
    {
        services.AddDbContext<IDatabase,DatabaseContext>(options => options.UseInMemoryDatabase(databaseName: inMemoryDbName));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
    }
}