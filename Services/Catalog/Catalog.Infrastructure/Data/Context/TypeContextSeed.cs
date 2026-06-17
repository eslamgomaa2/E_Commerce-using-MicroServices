using Catalog.Core.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Context
{
    public static class TypeContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductType> producttype)
        {
            var hasdata = await producttype.AsQueryable().AnyAsync();
            if (hasdata)
                return;
            var filepath = Path.Combine("Data", "SeedData", "types.json");
            if (!File.Exists(filepath))
            {
                Console.WriteLine("the file doesnt exist in this path");
            }
            var data = File.ReadAllText(filepath);
            var types = JsonSerializer.Deserialize<List<ProductType>>(data);
            if (types.Any() is true)
                await producttype.InsertManyAsync(types);
        }
    }
}
