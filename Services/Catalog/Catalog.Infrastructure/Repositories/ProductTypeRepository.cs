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
    }
}
