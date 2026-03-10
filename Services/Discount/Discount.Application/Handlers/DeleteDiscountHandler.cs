using Discount.Application.Commands;
using Discount.Application.Responses;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers
{
    public class DeleteDiscountHandler : IRequestHandler<DeleteDiscountCommand, Response>
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DeleteDiscountHandler> _logger;

        public DeleteDiscountHandler(IDiscountRepository repository, ILogger<DeleteDiscountHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Response> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await _repository.GetDiscount(request.ProductName);
            if (discount == null)
            {
                _logger.LogError("Discount with ProductName: {ProductName} not found.", request.ProductName);
                throw new RpcException(new Status(StatusCode.Internal, $"Could not found Discount for product {request.ProductName}."));
            }

            var deleted = await _repository.DeleteDiscount(request.ProductName);
            if (!deleted)
            {
                _logger.LogError("Failed to delete Discount with ProductName: {ProductName}.", request.ProductName);
                throw new RpcException(new Status(StatusCode.Internal, $"Failed to delete Discount for product {request.ProductName}."));
            }
            return new Response
            {
                Success = true,
                Message = $"Discount for product {request.ProductName} deleted successfully."
            };
        }
    }
}
