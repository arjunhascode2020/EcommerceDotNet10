using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Commands
{
    public record CreateDiscountCommand(string ProductName, string Discription, double Amount) : IRequest<CouponDto>;

}
