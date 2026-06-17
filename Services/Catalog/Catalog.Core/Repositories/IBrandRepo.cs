using Catalog.Core.Entities;

namespace Catalog.Core.Repositories
{
    public interface IBrandRepo
    {
        Task<IEnumerable<ProductBrand>> GetAllBrands();
    }
}
