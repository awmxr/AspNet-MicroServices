using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Threading;

namespace Discount.Grpc.Extensions
{
    public static class MigrationExtensions
    {
        public static void MigrateDatabase(this IServiceProvider services, int retry = 0)
        {
            int retryForAvailability = retry;
            using var scope = services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>(); // ✅ FIXED: Using ILogger<Program>

            try
            {
                logger.LogInformation("Migrating PostgreSQL database...");

                using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();

                using var command = new NpgsqlCommand { Connection = connection };

                command.CommandText = "DROP TABLE IF EXISTS Coupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
                                                            ProductName VARCHAR(200) NOT NULL,
                                                            Description TEXT,
                                                            Amount INT)";
                command.ExecuteNonQuery();

                // Seed data
                command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES ('IPhone X', 'iPhone discount', 150);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES ('Samsung 10', 'Samsung discount', 150);";
                command.ExecuteNonQuery();

                logger.LogInformation("Migration has been completed!!!");
            }
            catch (NpgsqlException ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database.");

                if (retryForAvailability < 5) // Prevent infinite recursion
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    services.MigrateDatabase(retryForAvailability);
                }
            }
        }
    }
}
