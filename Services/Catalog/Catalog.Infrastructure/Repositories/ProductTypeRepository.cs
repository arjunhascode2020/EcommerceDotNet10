using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly IMongoCollection<ProductType> _types;

        public ProductTypeRepository(IOptions<DatabaseSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _types = database.GetCollection<ProductType>(settings.TypeCollectionName);
        }

        public async Task<ProductType> CreateProductType(ProductType productType)
        {
            await _types.InsertOneAsync(productType);
            return productType;
        }

        public async Task<bool> Delete(string id)
        {
            var deleteTypes = await _types.DeleteOneAsync(id);
            return deleteTypes.IsAcknowledged && deleteTypes.DeletedCount > 0;
        }

        public async Task<ProductType> GetProductTypeByIdAsync(string id)
        {
            return await _types.
                Find(t => t.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductType>> GetProductTypesAsync()
        {
            return await _types.Find(_ => true)
                         .ToListAsync();
        }

        public async Task<bool> UpdateProdcutType(ProductType productType)
        {
            var updateType = await _types.ReplaceOneAsync(t => t.Id == productType.Id, productType);
            return updateType.IsAcknowledged && updateType.IsModifiedCountAvailable;
        }
    }
}
