using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.UniteOfWorks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers
{
    public class GetProductByNameHandler : IRequestHandler<GetProductByNameQuery, IReadOnlyList<ProductResponse>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<GetProductByNameHandler> _logger;

        public GetProductByNameHandler(IUniteOfWork uniteOfWork, ILogger<GetProductByNameHandler> logger)
        {
            _uniteOfWork = uniteOfWork;
            _logger = logger;
        }
        public async Task<IReadOnlyList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await _uniteOfWork.Products.GetProductsByNameAsync(request.productName);

            _logger.LogInformation($"Getting Products by productName:{request.productName},& total Products:{productList.Count()}");
            return productList.ToResponseList();
        }
    }
}
