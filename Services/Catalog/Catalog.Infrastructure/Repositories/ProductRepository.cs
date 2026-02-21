using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Settings;
using Catalog.Core.Specifications;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IOptions<DatabaseSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>(settings.ProductCollectionName);
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            await _products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var deletedProduct = await _products.DeleteOneAsync(p => p.Id == id);

            return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _products.Find(_ => true)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _products.Find(p => p.Id == id)
                   .FirstOrDefaultAsync();
        }

        public async Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams catalogSpecParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;
            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                filter &= builder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression($".*{catalogSpecParams.Search}.*", "i"));
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
            {
                filter &= builder.Eq(p => p.Brand.Id, catalogSpecParams.BrandId);
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
            {
                filter &= builder.Eq(p => p.Type.Id, catalogSpecParams.TypeId);
            }
            var totalItems = await _products.CountDocumentsAsync(filter);
            var data = await ApplyDataFilter(catalogSpecParams, filter);
            return new Pagination<Product>(

                catalogSpecParams.PageIndex,
                catalogSpecParams.PageSize,
              (int)totalItems,
                 data
            );
        }



        public async Task<IEnumerable<Product>> GetProductsByBrandAsync(string brandName)
        {
            return await _products.Find(p => p.Brand.Name.ToLower() == brandName.ToLower())
                        .ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            var filter = Builders<Product>.Filter.Regex(p => p.Name,
                new MongoDB.Bson.BsonRegularExpression($".*{name}.*", "i")
            );
            return await _products.Find(filter)
                         .ToListAsync();


        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return true;
        }

        private async Task<IReadOnlyCollection<Product>> ApplyDataFilter(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
        {
            var sortDefn = Builders<Product>.Sort.Ascending("Name");

            if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
            {
                sortDefn = catalogSpecParams.Sort switch
                {
                    "priceAsc" => Builders<Product>.Sort.Ascending(p => p.Price),
                    "priceDesc" => Builders<Product>.Sort.Descending(p => p.Price),
                    _ => Builders<Product>.Sort.Ascending(p => p.Name)
                };
            }

            return await _products.Find(filter)
                .Sort(sortDefn)
                .Skip((catalogSpecParams.PageIndex - 1) * catalogSpecParams.PageSize)
                .Limit(catalogSpecParams.PageSize)
                .ToListAsync();
        }
    }
}
