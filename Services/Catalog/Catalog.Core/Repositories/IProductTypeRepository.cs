using Catalog.Core.Entities;

namespace Catalog.Core.Repositories
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ProductType>> GetProductTypesAsync();
        Task<ProductType> GetProductTypeByIdAsync(string id);

        Task<ProductType> CreateProductType(ProductType productType);
        Task<bool> UpdateProdcutType(ProductType productType);

        Task<bool> Delete(string id);
    }
}
