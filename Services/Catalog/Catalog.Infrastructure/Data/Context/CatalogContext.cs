using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Context
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<ProductType> Types { get; }

        public IMongoCollection<ProductBrand> Brands { get; }
        public CatalogContext(IConfiguration configuration)
        {

            var connectionString = configuration["DatabaseSettings:ConnectionString"];
            var databaseName = configuration["DatabaseSettings:DatabaseName"];


            var client = new MongoClient(connectionString);

            // 3. تحديد قاعدة البيانات المستهدفة
            var database = client.GetDatabase(databaseName);

            // 4. ربط الخصائص بالمجموعات الفعلية في الداتابيز
            // (إذا لم تكن المجموعات موجودة، موندو سيقوم بإنشائها تلقائياً بمجرد إدخال بيانات)
            Products = database.GetCollection<Product>("Products");
            Brands = database.GetCollection<ProductBrand>("Brands");
            Types = database.GetCollection<ProductType>("Types");

            _ = ProductContextSeed.SeedDataAsync(Products);
            _ = TypeContextSeed.SeedDataAsync(Types);
            _ = BrandContextSeed.SeedDataAsync(Brands);
        }
    }
}
