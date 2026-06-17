using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data.Context;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProducttypeRepo, IBrandRepo, IProductRepo
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
            return product;

        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult = await _catalogContext.Products
                .DeleteOneAsync(p => p.Id == id);

            // نتحقق أن عملية الحذف تمت بنجاح
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _catalogContext.Brands.Find(p => true).ToListAsync();
        }

        public async Task<Pagination<Product>> GetAllProduct(CatalogSpecParams pram)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(pram.Search))
            {
                filter = filter & builder.Where(p => p.Name.ToLower() == pram.Search.ToLower());
            }

            if (!string.IsNullOrEmpty(pram.TypeId))
            {
                filter = filter & builder.Eq(o => o.Type.Id, pram.TypeId);  
            }

            if (!string.IsNullOrEmpty(pram.BrandId))
            {
                filter = filter & builder.Eq(o => o.Brand.Id, pram.BrandId);  
            }

            var filteredData = _catalogContext.Products.Find(filter);
            var count = await filteredData.CountDocumentsAsync();
            var data = await DataFilter(pram, filter);

            return new Pagination<Product>(
                pram.PageIndex,
                pram.PageSize,
                count,
                data
            );
        }

        public async Task<IEnumerable<ProductType>> GetAllProductType()
        {
            return await _catalogContext.Types.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            var proudct = await _catalogContext.Products.Find(o => o.Id == id).FirstOrDefaultAsync();
            return proudct;
        }

        public async Task<Product> GetProductByName(string name)
        {
            var proudct = await _catalogContext.Products.Find(o => o.Name == name).FirstOrDefaultAsync();
            return proudct;
        }

        public async Task<IEnumerable<Product>> GetProductsByBrand(string brand)
        {
            var proudct = await _catalogContext.Products.Find(o => o.Brand.Name == brand).ToListAsync();
            return proudct;
        }

        public async Task<IEnumerable<Product>> GetProductsByType(string type)
        {
            var proudct = await _catalogContext.Products.Find(o => o.Type.Name == type).ToListAsync();
            return proudct;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _catalogContext.Products
                 .ReplaceOneAsync(p => p.Id == product.Id, product);

            // نتحقق أن موندو استقبلت الأمر وقامت بتعديل سجل واحد على الأقل
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParams prams, FilterDefinition<Product> filter)
        {
            var sortDef = Builders<Product>.Sort.Ascending(p => p.Name);

            if (!string.IsNullOrEmpty(prams.Sort))
            {
                switch (prams.Sort)
                {
                    case "PriceAsc":
                        sortDef = Builders<Product>.Sort.Ascending(p => p.Price);
                        break;
                    case "PriceDesc":                                             
                        sortDef = Builders<Product>.Sort.Descending(p => p.Price);
                        break;
                    default:
                        sortDef = Builders<Product>.Sort.Ascending(p => p.Name);
                        break;
                }
            }

            return await _catalogContext.Products
                .Find(filter)
                .Sort(sortDef)
                .Skip(prams.PageSize * (prams.PageIndex - 1))  // ✅ was PageSize * PageIndex - 1 (wrong operator precedence)
                .Limit(prams.PageSize)                          // ✅ was missing — without this, ALL remaining docs returned
                .ToListAsync();
        }
    }
}
