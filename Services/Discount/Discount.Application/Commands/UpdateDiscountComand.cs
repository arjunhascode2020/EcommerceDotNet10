using Discount.Application.Responses;
using MediatR;

namespace Discount.Application.Commands
{
    public record UpdateDiscountComand(int Id, string ProductName, string Description, double Amount) : IRequest<Response>;

}
