using Catalog.Core.Entities;

namespace Catalog.Core.Repositories
{
    public interface IProductBrandRepository
    {
        Task<IEnumerable<ProductBrand>> GetProductBrandsAsync();
        Task<ProductBrand> GetProductBrandByIdAsync(string id);

        Task<ProductBrand> CreateBrand(ProductBrand productBrand);
        Task<bool> UpdateBrand(ProductBrand productBrand);

        Task<bool> DeleteBrandAsync(string id);
    }
}
