using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.UniteOfWorks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IUniteOfWork uniteOfWork, ILogger<CreateProductCommandHandler> logger)
        {
            _uniteOfWork = uniteOfWork;
            _logger = logger;
        }
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var brand = await _uniteOfWork.Brands.GetProductBrandByIdAsync(request.BrandId);
            var type = await _uniteOfWork.Types.GetProductTypeByIdAsync(request.TypeId);
            if (brand is null || type is null)
            {
                _logger.LogWarning($"Invaid Product Brand Id:{request.BrandId} or Type Id :{request.TypeId}.");
                throw new ArgumentNullException($"Invalide bandId or typeId Specified in Prodcut Creation.");
            }

            var productEntity = request.CommandToEntity(brand, type);
            var createdProdcut = await _uniteOfWork.Products.CreateProductAsync(productEntity);
            _logger.LogInformation($"Prodcut {request.Name} Created with id:{productEntity.Id}");
            return productEntity.ToResponse();


        }
    }
}
