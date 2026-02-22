using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands
{
    public record UpdateTypeCommand : IRequest<Response>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
