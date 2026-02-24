using Basket.Application.Commands;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Handlers
{
    public class DeleteBasketByUserHandler : IRequestHandler<DeleteBasketByUserCommand, Response>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<DeleteBasketByUserHandler> _logger;

        public DeleteBasketByUserHandler(IBasketRepository basketRepository, ILogger<DeleteBasketByUserHandler> logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
        }
        public async Task<Response> Handle(DeleteBasketByUserCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _basketRepository.DeleteBasket(request.userName);
            if (deleted)
            {
                _logger.LogInformation("Basket for user {UserName} deleted successfully.", request.userName);
                return new Response
                {
                    Success = true,
                    Message = $"Basket for user {request.userName} deleted successfully."
                };
            }

            return new Response
            {
                Success = false,
                Message = $"Failed to delete basket for user {request.userName}."
            };
        }
    }
}
