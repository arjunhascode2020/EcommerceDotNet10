using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands
{
    public class UpdateProductCommand : IRequest<Response>
    {
        public string Id { get; set; }
        public string Name { get; init; } = string.Empty;

        public string Summary { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public string ImageFile { get; init; } = string.Empty;

        public string BrandId { get; init; } = string.Empty;

        public string TypeId { get; init; } = string.Empty;
        public decimal Price { get; init; } = 0;
    }
}
