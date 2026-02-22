using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specifications;
using Catalog.Core.UniteOfWorks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, Pagination<ProductResponse>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<GetAllProductHandler> _logger;

        public GetAllProductHandler(IUniteOfWork uniteOfWork, ILogger<GetAllProductHandler> logger)
        {
            _uniteOfWork = uniteOfWork;
            _logger = logger;
        }
        public async Task<Pagination<ProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var paginatedProduct = await _uniteOfWork.Products.GetProductsAsync(request.SpecParams);
            _logger.LogInformation($"Getting Paginated Product PangeIndex:{paginatedProduct.PageIndex}, PageSize:{paginatedProduct.PageSize},Count:{paginatedProduct.Count}");
            return paginatedProduct.ToResponse();
        }
    }
}
