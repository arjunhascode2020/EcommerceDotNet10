using Catalog.Application.Commands;
using Catalog.Application.Exceptions;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.UniteOfWorks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IUniteOfWork uniteOfWork, ILogger<DeleteProductCommandHandler> logger)
        {
            _uniteOfWork = uniteOfWork;
            _logger = logger;
        }
        public async Task<Response> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var isProdcutExist = await _uniteOfWork.Products.GetProductByIdAsync(request.productId);
            if (isProdcutExist is null)
            {
                _logger.LogWarning($"Try to delete an product that was currently not exist,ProductId:{request.productId}");
                throw new NotFoundException(nameof(Product), request.productId);
            }
            var deleteResp = await _uniteOfWork.Products.DeleteProductAsync(request.productId);
            if (deleteResp)
            {
                return new Response
                {
                    Success = true,
                    Message = $"Prodcut Name:{isProdcutExist.Name} with Id:{isProdcutExist.Id} deleted Successfully."
                };
            }
            return new Response
            {
                Success = false,
                Message = $"Prodcut Name:{isProdcutExist.Name} with Id:{isProdcutExist.Id} deleted unsuccessfully. Try Again."
            };
        }
    }
}
