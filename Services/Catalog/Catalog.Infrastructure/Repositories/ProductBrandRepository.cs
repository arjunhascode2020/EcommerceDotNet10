using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductBrandRepository : IProductBrandRepository
    {
        private readonly IMongoCollection<ProductBrand> _brands;

        public ProductBrandRepository(IOptions<DatabaseSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _brands = database.GetCollection<ProductBrand>(settings.BrandCollectionName);
        }

        public async Task<ProductBrand> CreateBrand(ProductBrand productBrand)
        {
            await _brands.InsertOneAsync(productBrand);
            return productBrand;
        }

        public async Task<bool> DeleteBrandAsync(string id)
        {
            var deleteBrand = await _brands.DeleteOneAsync(id);
            return deleteBrand.IsAcknowledged && deleteBrand.DeletedCount > 0;
        }

        public async Task<ProductBrand> GetProductBrandByIdAsync(string id)
        {
            return await _brands.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductBrand>> GetProductBrandsAsync()
        {
            return await _brands.Find(_ => true).ToListAsync();
        }

        public async Task<bool> UpdateBrand(ProductBrand productBrand)
        {
            var updateBrand = await _brands.ReplaceOneAsync(b => b.Id == productBrand.Id, productBrand);
            return updateBrand
                .IsAcknowledged &&
                updateBrand.IsModifiedCountAvailable;
        }
    }
}
