using Catalog.Core.Entities;
using Catalog.Core.Specs;

namespace Catalog.Core.Repositories
{
    public interface IProductRepo
    {
        Task<Pagination<Product>> GetAllProduct(CatalogSpecParams pram);
        Task<Product> GetProductById(string id);
        Task<Product> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductsByBrand(string brand);
        Task<IEnumerable<Product>> GetProductsByType(string type);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
