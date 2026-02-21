using Catalog.Core.Repositories;

namespace Catalog.Core.UniteOfWork
{
    public interface IUniteOfWork
    {
        public IProductRepository Products { get; set; }
        public IProductBrandRepository Brands { get; set; }

        public IProductTypeRepository Types { get; set; }

    }
}
