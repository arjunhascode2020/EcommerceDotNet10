using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.UniteOfWorks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers
{
    public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IReadOnlyList<TypesResponse>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<GetAllTypesHandler> _logger;

        public GetAllTypesHandler(IUniteOfWork uniteOfWork, ILogger<GetAllTypesHandler> logger)
        {
            _uniteOfWork = uniteOfWork;
            _logger = logger;
        }
        public async Task<IReadOnlyList<TypesResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var productTypes = await _uniteOfWork.Types.GetProductTypesAsync();
            _logger.LogInformation($"Featched Product Type Count:{productTypes.Count()}");

            return productTypes.ToResponseList();

        }
    }
}
