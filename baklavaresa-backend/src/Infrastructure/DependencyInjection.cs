using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void SetupDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IDatabase, DatabaseContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
    }
}