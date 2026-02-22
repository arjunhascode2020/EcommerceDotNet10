using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands
{
    public record DeleteProductCommand(string productId) : IRequest<Response>;

}
