using Catalog.Core.Repositories;
using Catalog.Core.UniteOfWorks;

namespace Catalog.Infrastructure.UniteOfWorks
{
    public class UniteOfWork : IUniteOfWork
    {
        public IProductRepository Products { get; }
        public IProductBrandRepository Brands { get; }
        public IProductTypeRepository Types { get; }

        public UniteOfWork(IProductBrandRepository productBrandRepository, IProductRepository productRepository, IProductTypeRepository productTypeRepository)
        {
            Products = productRepository;
            Brands = productBrandRepository;
            Types = productTypeRepository;
        }
    }
}
