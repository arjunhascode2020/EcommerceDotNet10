using Catalog.Application.Commands;
using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.UniteOfWorks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IUniteOfWork uniteOfWork, ILogger<UpdateProductCommandHandler> logger)
        {
            this._uniteOfWork = uniteOfWork;
            this._logger = logger;
        }
        public async Task<Response> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var isProductExist = await _uniteOfWork.Products.GetProductByIdAsync(request.Id);
            if (isProductExist is null)
            {
                _logger.LogInformation($"Invalid Prodcut Name:{request.Name} Update Request with ProdcutId:{request.Id} not found");
                throw new NotFoundException(nameof(Product), request.Id);
            }
            var brand = await _uniteOfWork.Brands.GetProductBrandByIdAsync(request.BrandId);
            var type = await _uniteOfWork.Types.GetProductTypeByIdAsync(request.TypeId);
            if (brand is null || type is null)
            {
                _logger.LogWarning($"Invalid Prodcut Update request typeId:{request.TypeId} or branId:{request.BrandId}.");
                throw new ArgumentNullException($"Invalid Product TypeId :{request.TypeId} or BrandId{request.BrandId}.");
            }
            var productEntity = request.UpdateCommandToEntity(brand, type);
            var updateProduct = await _uniteOfWork.Products.UpdateProductAsync(productEntity);
            if (updateProduct)
            {
                return new Response
                {
                    Success = true,
                    Message = $"Prodcut Name:{isProductExist.Name} & Id:{isProductExist.Id} updated Successfully."
                };
            }
            return new Response
            {
                Success = false,
                Message = $"Prodcut Name:{isProductExist.Name} & Id:{isProductExist.Id} updated Faild."
            };
        }
    }
}
