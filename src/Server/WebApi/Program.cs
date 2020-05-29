namespace WebApi
{
    using EFCore.AutomaticMigrations;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using WebApi.Models;

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();
                try
                {
                    var environment = services.GetRequiredService<IWebHostEnvironment>();

                    if (environment.IsDevelopment())
                    {
                        var context = services.GetRequiredService<Infrastructure.AppDbContext>();
                        MigrateDatabaseToLatestVersion.ExecuteAsync(context).Wait();
                    }
                }
                catch (AppException ex)
                {
                    logger.LogError(ex, "An error occurred creating/updating the DB.");
                }
            }

            host.Run();
        }

        private static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
