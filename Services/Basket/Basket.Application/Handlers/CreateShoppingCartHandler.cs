using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<CreateShoppingCartHandler> _logger;

        public CreateShoppingCartHandler(IBasketRepository basketRepository, ILogger<CreateShoppingCartHandler> logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCartEntity = request.ToEntity();

            var createdCart = await _basketRepository.UpsertBasket(shoppingCartEntity);

            var response = createdCart.ToResponse();
            _logger.LogInformation("Created shopping cart for user {UserName} with {ItemCount} items.", response.UserName, response.Items.Count);
            return response;

        }
    }
}
