using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Discount.Infrastructure.Settings
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("DbExtension");
            var databaseSettings = services.GetRequiredService<IOptions<DatabaseSettings>>().Value;

            try
            {
                logger.LogInformation("Discount Db Migration Start......");
                //  logger.LogInformation($"Connection String :{databaseSettings.ConnectionString}");
                ApplyMigration(databaseSettings.ConnectionString);
                logger.LogInformation("Discount Db Migration End......");
            }
            catch (Exception ex)
            {

                logger.LogError("Discount Db Migration Failed......{Error}", ex.Message);
                throw; // Rethrow the exception to ensure it is not silently swallowed
            }
            return host;
        }

        private static void ApplyMigration(string connectionString)
        {
            var retryCount = 5;
            while (retryCount > 0)
            {
                try
                {
                    using var connection = new NpgsqlConnection(connectionString);
                    connection.Open();
                    using var command = new NpgsqlCommand
                    {
                        Connection = connection,
                    };
                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();
                    command.CommandText = @"CREATE TABLE Coupon(
                        Id SERIAL PRIMARY KEY,
                        ProductName VARCHAR(24) NOT NULL,
                        Description TEXT,
                        Amount INT
                    )";
                    command.ExecuteNonQuery();
                    command.CommandText = @"
                                            INSERT INTO Coupon (ProductName, Description, Amount) 
                                            VALUES ('IPhone X', 'IPhone Discount', 150);";
                    command.ExecuteNonQuery();
                    command.CommandText = @"
                                            INSERT INTO Coupon (ProductName, Description, Amount) 
                                            VALUES ('Samsung 10', 'Samsung Discount', 100);";
                    command.ExecuteNonQuery();
                    break; // Migration successful, exit the loop
                }
                catch
                {

                    retryCount--;
                    if (retryCount == 0)
                    {
                        throw; // Rethrow the exception if all retries have been exhausted
                    }

                }
            }
        }
    }
}
