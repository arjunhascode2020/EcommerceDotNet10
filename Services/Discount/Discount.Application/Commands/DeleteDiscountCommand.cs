using Discount.Application.Responses;
using MediatR;

namespace Discount.Application.Commands
{
    public record DeleteDiscountCommand(string ProductName) : IRequest<Response>;

}
