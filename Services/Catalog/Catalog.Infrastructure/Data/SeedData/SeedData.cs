using Catalog.Core.Entities;
using Catalog.Core.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace Catalog.Infrastructure.Data.SeedData
{
    public static class SeedData
    {
        public static async Task SeedDatabaseAsync(IOptions<DatabaseSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            await SeedProductBrandsAsync(database, settings.BrandCollectionName);
            await SeedProductTypesAsync(database, settings.TypeCollectionName);
            await SeedProductsAsync(database, settings.ProductCollectionName, settings.BrandCollectionName, settings.TypeCollectionName);
        }

        public static async Task SeedProductBrandsAsync(IMongoDatabase database, string collectionName)
        {
            var collection = database.GetCollection<ProductBrand>(collectionName);

            var existingCount = await collection.CountDocumentsAsync(FilterDefinition<ProductBrand>.Empty);
            if (existingCount > 0)
            {
                Console.WriteLine("ProductBrands collection already seeded.");
                return;
            }

            var brands = new List<ProductBrand>
        {
            new ProductBrand { Name = "Nike" },
            new ProductBrand { Name = "Adidas" },
            new ProductBrand { Name = "Puma" },
            new ProductBrand { Name = "Reebok" },
            new ProductBrand { Name = "Under Armour" },
            new ProductBrand { Name = "New Balance" },
            new ProductBrand { Name = "Asics" },
            new ProductBrand { Name = "Converse" },
            new ProductBrand { Name = "Vans" },
            new ProductBrand { Name = "Skechers" },
            new ProductBrand { Name = "Fila" },
            new ProductBrand { Name = "Jordan" },
            new ProductBrand { Name = "Champion" },
            new ProductBrand { Name = "The North Face" },
            new ProductBrand { Name = "Columbia" }
        };

            await collection.InsertManyAsync(brands);
            Console.WriteLine($"Seeded {brands.Count} product brands.");
        }

        public static async Task SeedProductTypesAsync(IMongoDatabase database, string collectionName)
        {
            var collection = database.GetCollection<ProductType>(collectionName);

            var existingCount = await collection.CountDocumentsAsync(FilterDefinition<ProductType>.Empty);
            if (existingCount > 0)
            {
                Console.WriteLine("ProductTypes collection already seeded.");
                return;
            }

            var types = new List<ProductType>
        {
            new ProductType { Name = "Shoes" },
            new ProductType { Name = "T-Shirts" },
            new ProductType { Name = "Hoodies" },
            new ProductType { Name = "Jackets" },
            new ProductType { Name = "Pants" },
            new ProductType { Name = "Shorts" },
            new ProductType { Name = "Socks" },
            new ProductType { Name = "Hats" },
            new ProductType { Name = "Bags" },
            new ProductType { Name = "Accessories" },
            new ProductType { Name = "Sneakers" },
            new ProductType { Name = "Boots" },
            new ProductType { Name = "Sandals" },
            new ProductType { Name = "Jerseys" },
            new ProductType { Name = "Tracksuits" }
        };

            await collection.InsertManyAsync(types);
            Console.WriteLine($"Seeded {types.Count} product types.");
        }

        public static async Task SeedProductsAsync(IMongoDatabase database, string productCollectionName, string brandCollectionName, string typeColectionName)
        {
            var productCollection = database.GetCollection<Product>(productCollectionName);

            // Check if already seeded
            var existingCount = await productCollection.CountDocumentsAsync(FilterDefinition<Product>.Empty);
            if (existingCount > 0)
            {
                Console.WriteLine("Products collection already seeded.");
                return;
            }

            // Get existing brands and types to reference
            var brandCollection = database.GetCollection<ProductBrand>(brandCollectionName);
            var typeCollection = database.GetCollection<ProductType>(typeColectionName);

            var brands = await brandCollection.Find(FilterDefinition<ProductBrand>.Empty).ToListAsync();
            var types = await typeCollection.Find(FilterDefinition<ProductType>.Empty).ToListAsync();

            if (!brands.Any() || !types.Any())
            {
                Console.WriteLine("Please seed ProductBrands and ProductTypes first!");
                return;
            }

            // Helper to get brand/type by name
            var nike = brands.First(b => b.Name == "Nike");
            var adidas = brands.First(b => b.Name == "Adidas");
            var puma = brands.First(b => b.Name == "Puma");
            var reebok = brands.First(b => b.Name == "Reebok");
            var underArmour = brands.First(b => b.Name == "Under Armour");

            var shoes = types.First(t => t.Name == "Shoes");
            var tShirts = types.First(t => t.Name == "T-Shirts");
            var hoodies = types.First(t => t.Name == "Hoodies");
            var jackets = types.First(t => t.Name == "Jackets");
            var sneakers = types.First(t => t.Name == "Sneakers");

            // Seed Products
            var products = new List<Product>
        {
            new Product
            {
                Name = "Nike Air Max 270",
                Summary = "Comfortable and stylish running shoes",
                Description = "The Nike Air Max 270 features Nike's biggest heel Air unit yet for a super-soft ride that feels as impossible as it looks. Inspired by two icons of big Air, the Air Max 180 and Air Max 93, it's designed for the modern world.",
                ImageFile = "nike-air-max-270.png",
                Brand = nike,
                Type = shoes,
                Price = 150.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Adidas Ultraboost 22",
                Summary = "Premium running shoes with boost technology",
                Description = "Experience energy return like never before. The adidas Ultraboost 22 running shoes feature a Linear Energy Push system for optimized energy return with every step.",
                ImageFile = "adidas-ultraboost-22.png",
                Brand = adidas,
                Type = sneakers,
                Price = 180.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Puma RS-X³",
                Summary = "Bold and chunky retro sneakers",
                Description = "The RS-X³ takes inspiration from the past while pushing boundaries with bold colors and exaggerated proportions. A perfect mix of comfort and style.",
                ImageFile = "puma-rsx3.png",
                Brand = puma,
                Type = sneakers,
                Price = 110.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Nike Dri-FIT Training T-Shirt",
                Summary = "Moisture-wicking performance tee",
                Description = "Stay dry and comfortable during your workouts with Nike Dri-FIT technology that wicks sweat away from your skin. Made with soft, breathable fabric.",
                ImageFile = "nike-dri-fit-tshirt.png",
                Brand = nike,
                Type = tShirts,
                Price = 35.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Adidas Essentials Hoodie",
                Summary = "Classic comfort hoodie for everyday wear",
                Description = "This hoodie is built for comfort with soft fleece fabric and a relaxed fit. Perfect for lounging or layering over your workout gear.",
                ImageFile = "adidas-essentials-hoodie.png",
                Brand = adidas,
                Type = hoodies,
                Price = 60.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Under Armour ColdGear Jacket",
                Summary = "Insulated jacket for cold weather training",
                Description = "UA ColdGear technology keeps you warm without weighing you down. Water-resistant fabric protects you from light rain while you train outdoors.",
                ImageFile = "ua-coldgear-jacket.png",
                Brand = underArmour,
                Type = jackets,
                Price = 120.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Reebok Classic Leather",
                Summary = "Timeless sneakers with premium leather",
                Description = "The Reebok Classic Leather is an icon of simplicity and style. Premium leather upper with a die-cut EVA midsole for lightweight cushioning.",
                ImageFile = "reebok-classic-leather.png",
                Brand = reebok,
                Type = sneakers,
                Price = 75.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Nike Tech Fleece Hoodie",
                Summary = "Innovative fleece with thermal warmth",
                Description = "Nike Tech Fleece delivers premium warmth without extra weight. Smooth on both sides with a spacer fabric that traps heat while reducing bulk.",
                ImageFile = "nike-tech-fleece-hoodie.png",
                Brand = nike,
                Type = hoodies,
                Price = 130.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Adidas Tiro Training Pants",
                Summary = "Slim-fit training pants with iconic 3-stripes",
                Description = "These soccer-inspired pants deliver a slim fit with ankle zips for easy on and off over shoes. Moisture-wicking Aeroready keeps you dry.",
                ImageFile = "adidas-tiro-pants.png",
                Brand = adidas,
                Type = types.First(t => t.Name == "Pants"),
                Price = 50.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Puma Ignite Running Shoes",
                Summary = "Responsive cushioning for your run",
                Description = "PUMA's IGNITE Foam technology provides energy return and responsive comfort. Designed for neutral pronators looking for a cushioned ride.",
                ImageFile = "puma-ignite.png",
                Brand = puma,
                Type = shoes,
                Price = 90.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Under Armour HeatGear T-Shirt",
                Summary = "Ultra-light performance tee",
                Description = "HeatGear fabric is ultra-soft and smooth for extreme comfort. Material wicks sweat and dries fast for all-day comfort.",
                ImageFile = "ua-heatgear-tshirt.png",
                Brand = underArmour,
                Type = tShirts,
                Price = 30.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Nike Revolution 6",
                Summary = "Affordable everyday running shoes",
                Description = "Lightweight and comfortable, the Nike Revolution 6 delivers a smooth ride for everyday runs. Soft foam midsole provides cushioning with every step.",
                ImageFile = "nike-revolution-6.png",
                Brand = nike,
                Type = shoes,
                Price = 65.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Adidas Originals Trefoil Hoodie",
                Summary = "Iconic style meets modern comfort",
                Description = "This hoodie celebrates adidas heritage with the classic Trefoil logo on the chest. French terry fabric keeps you comfortable all day long.",
                ImageFile = "adidas-trefoil-hoodie.png",
                Brand = adidas,
                Type = hoodies,
                Price = 70.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Reebok Nano X3",
                Summary = "Versatile training shoes for all workouts",
                Description = "The Reebok Nano X3 is built for varied training. Stable heel support for lifting and flexible forefoot for running and jumping.",
                ImageFile = "reebok-nano-x3.png",
                Brand = reebok,
                Type = shoes,
                Price = 140.00m,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Product
            {
                Name = "Puma Essentials Logo T-Shirt",
                Summary = "Classic cotton tee with PUMA branding",
                Description = "Simple and comfortable, this cotton t-shirt features the iconic PUMA logo. Perfect for everyday wear or casual workouts.",
                ImageFile = "puma-essentials-tshirt.png",
                Brand = puma,
                Type = tShirts,
                Price = 25.00m,
                CreatedDate = DateTimeOffset.UtcNow
            }
        };

            await productCollection.InsertManyAsync(products);
            Console.WriteLine($"Seeded {products.Count} products successfully.");
        }
    }
}
