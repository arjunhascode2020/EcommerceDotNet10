using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.UniteOfWorks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers
{
    public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, BrandResponse>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<CreateBrandHandler> _logger;

        public CreateBrandHandler(IUniteOfWork uniteOfWork, ILogger<CreateBrandHandler> logger)
        {
            this._uniteOfWork = uniteOfWork;
            this._logger = logger;
        }
        public async Task<BrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brandEntity = request.CommandToEntity();
            var createdBrand = await _uniteOfWork.Brands.CreateBrand(brandEntity);
            _logger.LogInformation($"Product Brand Name:{request.Name} created with id:{createdBrand.Id}");
            return createdBrand.ToResponse();
        }
    }
}
