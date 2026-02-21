using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductBrandRepository : IProductBrandRepository
    {
        private readonly IMongoCollection<ProductBrand> _brands;

        public ProductBrandRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            _brands = database.GetCollection<ProductBrand>(configuration["DatabaseSettings:BrandCollectionName"]);
        }
        public async Task<ProductBrand> GetProductBrandByIdAsync(string id)
        {
            return await _brands.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductBrand>> GetProductBrandsAsync()
        {
            return await _brands.Find(_ => true).ToListAsync();
        }
    }
}
