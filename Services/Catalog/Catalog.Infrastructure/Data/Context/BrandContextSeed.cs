using Catalog.Core.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Context
{
    public static class BrandContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductBrand> productBrand)
        {
            var hasdata = await productBrand.AsQueryable().AnyAsync();
            if (hasdata)
                return;
            var filepath = Path.Combine("Data", "SeedData", "brands.json");
            if (!File.Exists(filepath))
            {
                Console.WriteLine("the file doesnt exist in this path");
            }
            var data = File.ReadAllText(filepath);
            var types = JsonSerializer.Deserialize<List<ProductBrand>>(data);
            if (types.Any() is true)
                await productBrand.InsertManyAsync(types);
        }
    }
}
