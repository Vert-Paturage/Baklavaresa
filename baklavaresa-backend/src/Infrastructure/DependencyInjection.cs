using Domain.Repositories;
using Infrastructure.Data.Persistence;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void InfrastructureDependencies(this IServiceCollection services)
    {
        services.AddRepositories();
    }
    public static void SetupDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
    }
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
    }
}