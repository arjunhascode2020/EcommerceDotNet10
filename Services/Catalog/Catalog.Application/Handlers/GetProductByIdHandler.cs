using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.UniteOfWorks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<GetProductByIdQuery> _logger;

        public GetProductByIdHandler(IUniteOfWork uniteOfWork, ILogger<GetProductByIdQuery> logger)
        {
            _uniteOfWork = uniteOfWork;
            _logger = logger;
        }
        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _uniteOfWork.Products.GetProductByIdAsync(request.productId);
            _logger.LogInformation($"Get Product By Id :{request.productId} & product:{product.Name}.");
            return product.ToResponse();
        }
    }
}
