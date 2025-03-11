using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Api.Extentions
{
    public static class HostExtentions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext,IServiceProvider> seeder,int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                try
                {
                    logger.LogInformation("Migrating started for SqlServer");
                    InvokeSeeder(seeder, context, services);
                    logger.LogInformation("Migrated database SqlServer Done.");
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context");
                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryForAvailability);
                    }
                }
            }
            return host;

        }
        private static void InvokeSeeder<TContext>(Action<TContext,IServiceProvider> seeder,TContext context,IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context,services);
        }
    }
}
