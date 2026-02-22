using Catalog.Application.Responses;
using Catalog.Core.Specifications;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetAllProductQuery(CatalogSpecParams SpecParams) : IRequest<Pagination<ProductResponse>>;

}
