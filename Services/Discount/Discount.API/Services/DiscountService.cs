using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
namespace Discount.API.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;

        public DiscountService(IMediator mediator)
        {
            _mediator = mediator;
        }

        override public async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQuery(request.ProductName);
            var couponDto = await _mediator.Send(query);
            var couponModel = couponDto.ToModel();
            return couponModel;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var command = request.Coupon.ToCreateCommand();
            var couponDto = await _mediator.Send(command);
            return couponDto.ToModel();
        }
        public override async Task<ProtoResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var command = request.Coupon.ToUpdateCommand();
            var response = await _mediator.Send(command);
            return response.ToProtoResponse();
        }
        public override async Task<ProtoResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var command = new DeleteDiscountCommand(request.ProductName);
            var response = await _mediator.Send(command);
            return response.ToProtoResponse();
        }
    }
}
