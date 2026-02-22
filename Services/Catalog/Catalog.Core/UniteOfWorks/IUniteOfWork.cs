using Catalog.Core.Repositories;

namespace Catalog.Core.UniteOfWorks
{
    public interface IUniteOfWork
    {
        public IProductRepository Products { get; }
        public IProductBrandRepository Brands { get; }

        public IProductTypeRepository Types { get; }

    }
}
