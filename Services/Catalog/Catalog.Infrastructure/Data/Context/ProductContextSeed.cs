using Catalog.Core.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Context
{
    public static class ProductContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<Product> products)
        {
            var hasdata = await products.AsQueryable().AnyAsync();
            if (hasdata)
                return;
            var filepath = Path.Combine("Data", "SeedData", "Products.json");
            if (!File.Exists(filepath))
            {
                Console.WriteLine("the file doesnt exist in this path");
            }
            var data = File.ReadAllText(filepath);
            var types = JsonSerializer.Deserialize<List<Product>>(data);
            if (types.Any() is true)
                await products.InsertManyAsync(types);
        }
    }
}
