using Discount.Application.Commands;
using Discount.Application.Extensions;
using Discount.Application.Mappers;
using Discount.Application.Responses;
using Discount.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers
{
    public class UpdateDiscountHandler : IRequestHandler<UpdateDiscountComand, Response>
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<UpdateDiscountHandler> _logger;

        public UpdateDiscountHandler(IDiscountRepository repository, ILogger<UpdateDiscountHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Response> Handle(UpdateDiscountComand request, CancellationToken cancellationToken)
        {
            var validationErors = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(request.ProductName))
            {
                validationErors.Add(nameof(request.ProductName), "Product name is required.");
            }
            if (request.Amount <= 0)
            {
                validationErors.Add(nameof(request.Amount), "Amount must be greater than zero.");
            }
            if (string.IsNullOrEmpty(request.Description))
            {
                validationErors.Add(nameof(request.Description), "Description is required.");
            }
            if (validationErors.Any())
            {
                throw GrpcErrorHelper.CreateValidationException(validationErors);
            }

            var couponEntity = request.ToEntity();
            var updateResult = await _repository.UpdateDiscount(couponEntity);
            if (updateResult)
            {
                _logger.LogInformation("Discount updated successfully for ProductName: {ProductName}", request.ProductName);
                return new Response { Success = true, Message = "Discount updated successfully." };
            }
            else
            {
                _logger.LogWarning("Failed to update discount for ProductName: {ProductName}", request.ProductName);
                return new Response { Success = false, Message = "Failed to update discount." };
            }
        }
    }
}
