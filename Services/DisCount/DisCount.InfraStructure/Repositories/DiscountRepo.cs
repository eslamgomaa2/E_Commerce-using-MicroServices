using Dapper;
using DisCount.Core.Entities;
using DisCount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisCount.InfraStructure.Repositories
{
    public class DiscountRepo : IDiscountRepo
    {
        private readonly IConfiguration _configuration;

        public DiscountRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connections = await GetConnectionAsync();
            var query= "Insert into coupon (ProductName, Description, Amount) values (@ProductName, @Description, @Amount)";
            var res = await connections.ExecuteAsync(query, new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (res == 0)
            {
                return false;
            }
              return true;

        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connections = await GetConnectionAsync();
            var query = "Delete from coupon where ProductName=@productName";
            var res = await connections.ExecuteAsync(query, new { productName = productName });
            if (res == 0)
            {
                return false;
            }
            return true;

        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = await GetConnectionAsync();

            var query = "SELECT * FROM coupon WHERE ProductName = @ProductName";

            
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(query, new { ProductName = productName });

            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connections = await GetConnectionAsync();
            var query = "Update coupon set Description=@Description, Amount=@Amount where ProductName=@ProductName";
            var res = await connections.ExecuteAsync(query, new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (res == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<NpgsqlConnection> GetConnectionAsync()
        {
            // جلب الـ Connection String بالطريقة الصحيحة لـ .NET
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            return connection;
        }
    }
}
