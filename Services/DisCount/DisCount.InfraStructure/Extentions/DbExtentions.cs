using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace DisCount.InfraStructure.Extentions
{
    public static class DbExtentions
    {
        public static IHost ApplyMigrations(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            // ✅ استخدام GetConnectionString بدل GetValue
            var connectionString = config.GetConnectionString("DefaultConnection");

            // ✅ Debug Line مهمة جداً
            Console.WriteLine($"[DEBUG] ConnectionString = '{connectionString}'");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("ConnectionStrings:DefaultConnection is NULL! Check environment variables.");

            var retry = 5;
            while (retry > 0)
            {
                try
                {
                    using var connection = new NpgsqlConnection(connectionString);
                    connection.Open();

                    using var cmd = new NpgsqlCommand { Connection = connection };

                    cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"CREATE TABLE Coupon(
                                        ID SERIAL PRIMARY KEY,
                                        ProductName VARCHAR(500) NOT NULL,
                                        Description TEXT,
                                        Amount INT)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Egypt Adidas Quick Force Indoor Badminton Shoes', 'Adidas Discount', 600);";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('PowerFit 19 FH Rubber Spike Cricket Shoes', 'PowerFit Discount', 700);";
                    cmd.ExecuteNonQuery();

                    break;
                }
                catch (Exception ex)
                {
                    retry--;
                    Console.WriteLine($"[Retry {5 - retry}/5] Database not ready: {ex.Message}");
                    if (retry == 0) throw;
                    Thread.Sleep(2000);
                }
            }

            return host;
        }
    }
}
