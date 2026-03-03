using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Repositories;
using UserManagement.Persistence.Context;
using UserManagement.Persistence.Repositories;
using UserManagement.Persistence.Seed;

namespace UserManagement.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dbPath = configuration["DB_PATH"];

        var connectionString = !string.IsNullOrEmpty(dbPath)
            ? $"Data Source={dbPath}"
            : configuration.GetConnectionString("Sqlite");

        services.AddDbContext<DataContext>(opt => opt.UseSqlite(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<DbSeeder>();
    }
}