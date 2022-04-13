using EFCore.AutomaticMigrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class SetupPersistence
    {
        public static void ConfigureAppSqlDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(it =>
            {
                it.UseSqlServer(configuration["Database:ConnectionString"]);
            });
        }

        public static async Task MigrateCatalogDbToLatestVersionAsync(this IServiceScopeFactory scopeFactory)
        {
            await using var serviceScope = scopeFactory.CreateAsyncScope();
            await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.MigrateToLatestVersionAsync();
        }
    }
}
