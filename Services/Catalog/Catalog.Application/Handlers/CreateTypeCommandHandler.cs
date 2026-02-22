using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.UniteOfWorks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers
{
    public class CreateTypeCommandHandler : IRequestHandler<CreateTypeCommand, TypesResponse>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<CreateTypeCommandHandler> _logger;

        public CreateTypeCommandHandler(IUniteOfWork uniteOfWork, ILogger<CreateTypeCommandHandler> logger)
        {
            _uniteOfWork = uniteOfWork;
            _logger = logger;
        }
        public async Task<TypesResponse> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
        {
            var typeEntity = request.CommandToEntity();
            var createdType = await _uniteOfWork.Types.CreateProductType(typeEntity);
            _logger.LogInformation($"Prodcut Type Name:{request.Name} created with id:{createdType.Id}");

            return createdType.ToResponse();
        }
    }
}
