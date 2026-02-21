using Catalog.Core.Entities;

namespace Catalog.Core.Repositories
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ProductType>> GetProductTypesAsync();
        Task<ProductType> GetProductTypeByIdAsync(string id);
    }
}
