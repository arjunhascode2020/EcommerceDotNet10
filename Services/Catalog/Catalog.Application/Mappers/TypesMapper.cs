using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers
{
    public static class TypesMapper
    {

        public static TypesResponse ToResponse(this ProductType productType)
        {
            return new TypesResponse
            {
                Id = productType.Id,
                Name = productType.Name,
            };
        }
        public static IReadOnlyList<TypesResponse> ToResponseList(this IEnumerable<ProductType> productTypes)
        {
            return productTypes.Select(t => t.ToResponse()).ToList();
        }

        public static ProductType CommandToEntity(this CreateTypeCommand createTypeCommand) =>
            new ProductType
            {
                Name = createTypeCommand.Name,
            };
    }
}
