using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.UniteOfWorks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers
{
    public class GetProductByBrandHandler : IRequestHandler<GetProductByBrandQuery, IReadOnlyList<ProductResponse>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<GetProductByBrandHandler> _logger;

        public GetProductByBrandHandler(IUniteOfWork uniteOfWork, ILogger<GetProductByBrandHandler> logger)
        {
            _uniteOfWork = uniteOfWork;
            _logger = logger;
        }
        public async Task<IReadOnlyList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            var productList = await _uniteOfWork.Products.GetProductsByBrandAsync(request.brandName);
            _logger.LogInformation($"Getting Products by brandName:{request.brandName} & total products:{productList.Count()}");
            return productList.ToResponseList();
        }
    }
}
