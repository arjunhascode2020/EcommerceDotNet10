using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Commands
{
    public record DeleteBasketByUserCommand(string userName) : IRequest<Response>;

}
