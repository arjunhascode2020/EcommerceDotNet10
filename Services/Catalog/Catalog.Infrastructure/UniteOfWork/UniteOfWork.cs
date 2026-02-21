using Catalog.Core.Repositories;
using Catalog.Core.UniteOfWork;

namespace Catalog.Infrastructure.UniteOfWork
{
    public class UniteOfWork : IUniteOfWork
    {
        public IProductRepository Products { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IProductBrandRepository Brands { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IProductTypeRepository Types { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
