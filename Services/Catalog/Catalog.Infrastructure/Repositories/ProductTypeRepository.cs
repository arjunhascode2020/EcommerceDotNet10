using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly IMongoCollection<ProductType> _types;

        public ProductTypeRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            _types = database.GetCollection<ProductType>(configuration["DatabaseSettings:TypeCollectionName"]);
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
