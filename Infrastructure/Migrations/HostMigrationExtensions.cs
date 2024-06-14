using System.Reflection;
using DbUp;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Migrations.Extensions;

public static class HostMigrationExtensions
{
    public static void Migrate<TContext>(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<TContext>>();

        logger.LogInformation("Migrating database...");

        var connection = configuration.GetConnectionString("DefaultConnection");

        if (connection is null)
        {
            logger.LogError("Default connection string was not specified");
            return;
        }

        EnsureDatabase.For.PostgresqlDatabase(connection);

        var upgrader = DeployChanges.To
            .PostgresqlDatabase(connection)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            logger.LogError(result.Error, "An error occurred while migrating the database");
            return;
        }

        logger.LogInformation("Database migration has been completed");
    }
}