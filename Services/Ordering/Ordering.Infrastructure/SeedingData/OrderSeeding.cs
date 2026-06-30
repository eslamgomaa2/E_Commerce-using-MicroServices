using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using Ordering.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.SeedingData
{
    public static class OrderSeeding
    {
        public static async Task SeedAsync(ApplicationDbContext Dbcontext,ILogger<Order> logger)
        {
            var res = await GetOrders();
            if (!Dbcontext.Orders.Any())
            {
               
                await Dbcontext.Orders.AddRangeAsync(res);
                await Dbcontext.SaveChangesAsync();
                logger.LogInformation("Seeded database with initial data.");
            }
        }

        public static async Task<IEnumerable<Order>> GetOrders()  
        {
            return new List<Order>
            {
                new Order
                {
                    UserName = "Eslaaamgom",
                    FirstName = "Eslam",
                    LastName = "Gomaa",
                    EmailAddress = " @slamgoma@",
                    AddressLine="Tamalu",
                    CardName="Visa",
                    CardNumber="1234567890123456",
                    CVV="123",
                    Expiration="12/25",
                    Country="egypt",
                    TotalPrice=269,
                    CreatedBy="eslam",
                    PaymentMethod=1,
                    State="Cairo",
                    ZipCode="12345"
                }
            };  
        }
    }
}
