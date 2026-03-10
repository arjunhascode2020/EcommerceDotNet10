using Discount.Application.DTOs;
using Discount.Application.Extensions;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers
{
    public class GetDiscountHandler : IRequestHandler<GetDiscountQuery, CouponDto>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<GetDiscountHandler> _logger;

        public GetDiscountHandler(IDiscountRepository discountRepository, ILogger<GetDiscountHandler> logger)
        {
            _discountRepository = discountRepository;
            _logger = logger;
        }
        public async Task<CouponDto> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.productName))
            {
                var validationErrors = new Dictionary<string, string>
                {
                    { "ProdcutName",  "Product name must not be empty." }
                };
                throw GrpcErrorHelper.CreateValidationException(validationErrors);
            }
            var coupon = await _discountRepository.GetDiscount(request.productName);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Could not get Discount for product {request.productName}."));
            }

            return coupon.ToDto();
        }
    }
}
