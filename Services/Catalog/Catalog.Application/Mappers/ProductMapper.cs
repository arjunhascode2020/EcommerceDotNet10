using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specifications;

namespace Catalog.Application.Mappers
{
    public static class ProductMapper
    {
        public static ProductResponse ToResponse(this Product product)
        {

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Summary = product.Summary,
                Description = product.Description,
                ImageFile = product.ImageFile,
                Brand = product.Brand.ToResponse(),
                Types = product.Type.ToResponse(),
                Price = product.Price,
                CreatedDate = product.CreatedDate,
            };

        }

        public static Pagination<ProductResponse> ToResponse(this Pagination<Product> pagination)
        {
            return new Pagination<ProductResponse>(
                  pagination.PageIndex,
                  pagination.PageSize,
                  pagination.Count,
                  pagination.Data.Select(p => p.ToResponse()).ToList()
                );
        }

        public static IReadOnlyList<ProductResponse> ToResponseList(this IEnumerable<Product> products) =>
             products.Select(p => p.ToResponse()).ToList();

        public static Product CommandToEntity(this CreateProductCommand command, ProductBrand productBrand, ProductType productType)
        {
            return new Product
            {
                Name = command.Name,
                Summary = command.Summary,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Brand = productBrand,
                Type = productType,
                Price = command.Price,
            };
        }

        public static Product UpdateCommandToEntity(this UpdateProductCommand updateProductCommand, ProductBrand productBrand, ProductType productType)
        {
            return new Product
            {
                Id = updateProductCommand.Id,
                Name = updateProductCommand.Name,
                Summary = updateProductCommand.Summary,
                Description = updateProductCommand.Description,
                ImageFile = updateProductCommand.ImageFile,
                Brand = productBrand,
                Type = productType,
                Price = updateProductCommand.Price,
            };
        }



    }
}
