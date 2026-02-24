using Basket.Application.DTOs;
using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Commands
{
    public record CreateShoppingCartCommand(string userName, IList<ShoppingCardItemDto> items) : IRequest<ShoppingCartResponse>;

}
