using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands
{
    public record CreateTypeCommand : IRequest<TypesResponse>
    {
        public string Name { get; set; }
    }
}
