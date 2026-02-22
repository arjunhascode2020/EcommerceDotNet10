using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers
{
    public static class BrandMapper
    {
        public static BrandResponse ToResponse(this ProductBrand productBrand)
        {
            BrandResponse response = new BrandResponse
            {
                Id = productBrand.Id,
                Name = productBrand.Name,
            };
            return response;
        }

        public static IReadOnlyList<BrandResponse> ToResponseList(this IEnumerable<ProductBrand> productBrands)
        {
            return productBrands.Select(b => b.ToResponse()).ToList();
        }

        public static ProductBrand CommandToEntity(this CreateBrandCommand brandCommand) =>
            new ProductBrand
            {
                Name = brandCommand.Name,
            };

        public static ProductBrand UpdateBrandToEntity(this UpdateBrandCommand updateBrandCommand) =>
            new ProductBrand
            {
                Id = updateBrandCommand.Id,
                Name = updateBrandCommand.Name,
            };
    }
}
