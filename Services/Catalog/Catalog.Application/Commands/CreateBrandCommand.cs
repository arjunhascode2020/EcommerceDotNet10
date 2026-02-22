using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands
{
    public record CreateBrandCommand : IRequest<BrandResponse>
    {
        public string Name { get; set; }
    }
}
