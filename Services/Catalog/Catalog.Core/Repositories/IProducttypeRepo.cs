using Catalog.Core.Entities;

namespace Catalog.Core.Repositories
{
    public interface IProducttypeRepo
    {
        Task<IEnumerable<ProductType>> GetAllProductType();
    }
}
