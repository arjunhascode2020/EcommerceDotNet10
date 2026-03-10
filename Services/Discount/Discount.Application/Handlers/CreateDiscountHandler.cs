using Discount.Application.Commands;
using Discount.Application.DTOs;
using Discount.Application.Extensions;
using Discount.Application.Mappers;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers
{
    public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CouponDto>
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<CreateDiscountHandler> _logger;

        public CreateDiscountHandler(IDiscountRepository repository, ILogger<CreateDiscountHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<CouponDto> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var validationErrors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(request.ProductName))
                validationErrors.Add(nameof(request.ProductName), "Product name is required.");
            if (string.IsNullOrWhiteSpace(request.Discription))
                validationErrors.Add(nameof(request.Discription), "Description is required.");
            if (request.Amount <= 0)
                validationErrors.Add(nameof(request.Amount), "Amount must be greater than zero.");
            if (validationErrors.Any())
            {
                _logger.LogWarning("Validation failed for CreateDiscountCommand: {Errors}", validationErrors);
                throw GrpcErrorHelper.CreateValidationException(validationErrors);
            }
            var couponEntity = request.ToEntity();
            var createdCoupon = await _repository.CreateDiscount(couponEntity);
            if (!createdCoupon)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Failed to create discount for the prodcut {request.ProductName}."));
            }
            _logger.LogInformation("Discount created successfully for product {ProductName}", request.ProductName);
            return new CouponDto(0, couponEntity.ProductName, couponEntity.Description, couponEntity.Amount);

        }
    }
}
