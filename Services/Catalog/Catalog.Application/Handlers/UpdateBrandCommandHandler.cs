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
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Response>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<UpdateBrandCommandHandler> _logger;

        public UpdateBrandCommandHandler(IUniteOfWork uniteOfWork, ILogger<UpdateBrandCommandHandler> logger)
        {
            this._uniteOfWork = uniteOfWork;
            this._logger = logger;
        }
        async Task<Response> IRequestHandler<UpdateBrandCommand, Response>.Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var isBrandExist = await _uniteOfWork.Brands.GetProductBrandByIdAsync(request.Id);
            if (isBrandExist is null)
            {
                _logger.LogWarning($"Prodcut Brand With Id :{request.Id} Not Found.");
                throw new NotFoundException(nameof(ProductBrand), request.Id);
            }

            var brandEntity = request.UpdateBrandToEntity();
            var updatedBrand = await _uniteOfWork.Brands.UpdateBrand(brandEntity);
            if (updatedBrand)
            {
                return new Response
                {
                    Success = true,
                    Message = $"Prodcut Brand Name:{isBrandExist.Name}& Id:{isBrandExist.Id} is Updated successfully."
                };
            }

            return new Response
            {
                Success = false,
                Message = $"Prodcut Brand Name:{isBrandExist.Name}& Id:{isBrandExist.Id} is Updated faild."
            };
        }
    }
}
