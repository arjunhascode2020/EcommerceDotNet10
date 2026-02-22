using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.UniteOfWorks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IReadOnlyList<BrandResponse>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<GetAllBrandsHandler> _logger;

        public GetAllBrandsHandler(IUniteOfWork uniteOfWork, ILogger<GetAllBrandsHandler> logger)
        {
            _uniteOfWork = uniteOfWork;
            _logger = logger;
        }
        public async Task<IReadOnlyList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var branList = await _uniteOfWork.Brands.GetProductBrandsAsync();
            _logger.LogInformation($"Featched Product Brands Count:{branList.Count()}.");
            return branList.ToResponseList();
        }
    }
}
