using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.UniteOfWorks;

using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers
{
    public class UpdateTypeCommandHandler : IRequestHandler<UpdateTypeCommand, Response>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ILogger<UpdateTypeCommandHandler> _logger;

        public UpdateTypeCommandHandler(IUniteOfWork uniteOfWork, ILogger<UpdateTypeCommandHandler> logger)
        {
            _uniteOfWork = uniteOfWork;
            _logger = logger;
        }
        public Task<Response> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
