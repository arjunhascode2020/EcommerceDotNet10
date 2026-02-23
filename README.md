# E-Commerce API

A modern e-commerce REST API built with .NET Core 8 and MongoDB, featuring product management with brands and types.

## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Database Schema](#database-schema)
- [API Endpoints](#api-endpoints)
- [Running the Application](#running-the-application)
- [Seeding Data](#seeding-data)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

---

## ✨ Features

- ✅ **Product Management** - CRUD operations for products
- ✅ **Brand Management** - Manage product brands
- ✅ **Type Management** - Categorize products by type
- ✅ **MongoDB Integration** - NoSQL database with embedded documents
- ✅ **Auto Seeding** - Pre-populated sample data on first run
- ✅ **RESTful API** - Clean and intuitive API design
- ✅ **Async/Await** - Non-blocking I/O operations
- ✅ **Clean Architecture** - Separation of concerns

---

## 🛠️ Tech Stack

- **.NET Core 8** - Modern web framework
- **MongoDB** - NoSQL document database
- **MongoDB.Driver** - Official MongoDB driver for .NET
- **C# 12** - Latest C# features
- **Swagger/OpenAPI** - API documentation

---

## 📦 Prerequisites

Before running this project, ensure you have:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [MongoDB](https://www.mongodb.com/try/download/community) 6.0 or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [MongoDB Compass](https://www.mongodb.com/products/compass) (optional - for GUI)

---

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/ecommerce-api.git
cd ecommerce-api
```

### 2. Restore NuGet Packages

```bash
dotnet restore
```

### 3. Install MongoDB Driver

```bash
dotnet add package MongoDB.Driver
```

### 4. Start MongoDB

**Windows:**
```bash
net start MongoDB
```

**macOS/Linux:**
```bash
sudo systemctl start mongod
```

**Docker:**
```bash
docker run -d -p 27017:27017 --name mongodb mongo:latest
```

---

## ⚙️ Configuration

### appsettings.json

Update your `appsettings.json` with MongoDB connection details:

```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017"
  },
  "DatabaseName": "ECommerceDb",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Environment Variables (Optional)

For production, use environment variables:

```bash
export ConnectionStrings__MongoDb="mongodb://username:password@host:27017"
export DatabaseName="ECommerceDb"
```

---

## 📊 Database Schema

### Collections

#### **Products**
```json
{
  "_id": "ObjectId",
  "Name": "string",
  "Summary": "string",
  "Description": "string",
  "ImageFile": "string",
  "Brand": {
    "_id": "ObjectId",
    "Name": "string",
    "CreatedAt": "DateTime"
  },
  "Type": {
    "_id": "ObjectId",
    "Name": "string",
    "CreatedAt": "DateTime"
  },
  "Price": "Decimal128",
  "CreatedDate": "DateTimeOffset",
  "CreatedAt": "DateTime"
}
```

#### **ProductBrands**
```json
{
  "_id": "ObjectId",
  "Name": "string",
  "CreatedAt": "DateTime"
}
```

#### **ProductTypes**
```json
{
  "_id": "ObjectId",
  "Name": "string",
  "CreatedAt": "DateTime"
}
```

---

## 🔌 API Endpoints

### Products

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/products` | Get all products |
| GET | `/api/products/{id}` | Get product by ID |
| POST | `/api/products` | Create new product |
| PUT | `/api/products/{id}` | Update product |
| DELETE | `/api/products/{id}` | Delete product |

### Brands

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/brands` | Get all brands |
| GET | `/api/brands/{id}` | Get brand by ID |
| POST | `/api/brands` | Create new brand |
| PUT | `/api/brands/{id}` | Update brand |
| DELETE | `/api/brands/{id}` | Delete brand |

### Types

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/types` | Get all types |
| GET | `/api/types/{id}` | Get type by ID |
| POST | `/api/types` | Create new type |
| PUT | `/api/types/{id}` | Update type |
| DELETE | `/api/types/{id}` | Delete type |

---

## 🏃 Running the Application

### Development Mode

```bash
dotnet run
```

Or with hot reload:

```bash
dotnet watch run
```

### Production Mode

```bash
dotnet build -c Release
dotnet run --configuration Release
```

The API will be available at:
- **HTTP:** `http://localhost:5000`
- **HTTPS:** `https://localhost:5001`
- **Swagger:** `http://localhost:5000/swagger`

---

## 🌱 Seeding Data

The database is automatically seeded on first run with:

- **15 Product Brands** (Nike, Adidas, Puma, etc.)
- **15 Product Types** (Shoes, T-Shirts, Hoodies, etc.)
- **15 Sample Products** (with brand and type references)

### Manual Seeding

To manually trigger seeding:

```csharp
// In Program.cs
using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
    await SeedData.SeedDatabaseAsync(database);
}
```

### Clear Database

To reset and re-seed:

```bash
mongosh
use ECommerceDb
db.dropDatabase()
```

Then restart the application.

---

## 📁 Project Structure

```
ECommerceAPI/
├── Controllers/
│   ├── ProductsController.cs
│   ├── BrandsController.cs
│   └── TypesController.cs
├── Models/
│   ├── BaseEntity.cs
│   ├── Product.cs
│   ├── ProductBrand.cs
│   └── ProductType.cs
├── Data/
│   ├── SeedData.cs
│   └── MongoDbContext.cs
├── Services/
│   ├── IProductService.cs
│   └── ProductService.cs
├── Repositories/
│   ├── IRepository.cs
│   └── MongoRepository.cs
├── DTOs/
│   ├── ProductDto.cs
│   ├── CreateProductDto.cs
│   └── UpdateProductDto.cs
├── appsettings.json
├── Program.cs
└── README.md
```

---

## 📖 Sample API Requests

### Get All Products

```bash
curl -X GET "http://localhost:5000/api/products" -H "accept: application/json"
```

### Get Product by ID

```bash
curl -X GET "http://localhost:5000/api/products/6756a1b2c3d4e5f6a7b8c9e1" -H "accept: application/json"
```

### Create Product

```bash
curl -X POST "http://localhost:5000/api/products" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Nike Air Force 1",
    "summary": "Classic basketball sneakers",
    "description": "The Nike Air Force 1 is a timeless classic...",
    "imageFile": "nike-af1.png",
    "brandId": "6756a1b2c3d4e5f6a7b8c9d0",
    "typeId": "6756a1b2c3d4e5f6a7b8c9d1",
    "price": 120.00
  }'
```

### Update Product

```bash
curl -X PUT "http://localhost:5000/api/products/6756a1b2c3d4e5f6a7b8c9e1" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Nike Air Force 1 Updated",
    "price": 130.00
  }'
```

### Delete Product

```bash
curl -X DELETE "http://localhost:5000/api/products/6756a1b2c3d4e5f6a7b8c9e1"
```

---

## 🧪 Testing

### Unit Tests

```bash
dotnet test
```

### Integration Tests

```bash
dotnet test --filter Category=Integration
```

### Test with Postman

Import the Postman collection: [Download Collection](postman_collection.json)

---

## 🐳 Docker Support

### Build Docker Image

```bash
docker build -t ecommerce-api .
```

### Run with Docker Compose

```yaml
version: '3.8'
services:
  mongodb:
    image: mongo:latest
    ports:
      - "27017:27017"
    volumes:
      - mongodb_data:/data/db

  api:
    build: .
    ports:
      - "5000:8080"
    environment:
      - ConnectionStrings__MongoDb=mongodb://mongodb:27017
      - DatabaseName=ECommerceDb
    depends_on:
      - mongodb

volumes:
  mongodb_data:
```

Run:
```bash
docker-compose up
```

---

## 🔒 Security Best Practices

- ✅ Use environment variables for sensitive data
- ✅ Enable HTTPS in production
- ✅ Implement authentication & authorization (JWT)
- ✅ Add rate limiting
- ✅ Validate all inputs
- ✅ Use MongoDB connection pooling
- ✅ Implement CORS properly

---

## 🚀 Deployment

### Azure App Service

```bash
az webapp up --name ecommerce-api --resource-group myResourceGroup
```

### AWS Elastic Beanstalk

```bash
eb init -p "dotnet-core-8" ecommerce-api
eb create ecommerce-api-env
```

### Heroku

```bash
heroku create ecommerce-api
git push heroku main
```

---

## 📈 Performance Tips

1. **Indexing**: Create indexes on frequently queried fields
```javascript
db.Products.createIndex({ "Name": 1 })
db.Products.createIndex({ "Brand.Name": 1 })
db.Products.createIndex({ "Price": 1 })
```

2. **Pagination**: Implement pagination for large datasets
3. **Caching**: Use Redis for frequently accessed data
4. **Connection Pooling**: Configure MongoDB connection pool size

---

## 🐛 Troubleshooting

### MongoDB Connection Failed

```bash
# Check if MongoDB is running
sudo systemctl status mongod

# Check connection string
mongodb://localhost:27017
```

### Port Already in Use

```bash
# Kill process on port 5000
lsof -ti:5000 | xargs kill -9
```

### Seeding Issues

```bash
# Drop database and restart
mongosh
use ECommerceDb
db.dropDatabase()
```

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👥 Authors

- **Your Name** - *Initial work* - [YourGitHub](https://github.com/yourusername)

---

## 🙏 Acknowledgments

- MongoDB Team for excellent .NET driver
- .NET Team for the amazing framework
- Community contributors

---

## 📞 Support

For support, email support@example.com or join our Slack channel.

---

## 🔗 Useful Links

- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [MongoDB .NET Driver](https://mongodb.github.io/mongo-csharp-driver/)
- [REST API Best Practices](https://restfulapi.net/)
- [MongoDB Best Practices](https://www.mongodb.com/docs/manual/administration/production-notes/)

---

**Made with ❤️ using .NET Core and MongoDB**
