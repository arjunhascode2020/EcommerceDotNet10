using Basket.Application.Commands;
using Basket.Application.GrpcServices;
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
        private readonly DiscountGrpcService _discountGrpcService;

        public CreateShoppingCartHandler(IBasketRepository basketRepository, ILogger<CreateShoppingCartHandler> logger, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _logger = logger;
            _discountGrpcService = discountGrpcService;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            // Apply discounts to each item in the shopping cart
            foreach (var item in request.items)
            {
                var discount = await _discountGrpcService.GetDiscount(item.ProductName);
                if (discount != null)
                {
                    item.Price -= (decimal)discount.Amount;
                    _logger.LogInformation("Applied discount of {DiscountAmount} to product {ProductName}. New price: {NewPrice}", discount.Amount, item.ProductName, item.Price);
                }
            }
            var shoppingCartEntity = request.ToEntity();

            var createdCart = await _basketRepository.UpsertBasket(shoppingCartEntity);

            var response = createdCart.ToResponse();
            _logger.LogInformation("Created shopping cart for user {UserName} with {ItemCount} items.", response.UserName, response.Items.Count);
            return response;

        }
    }
}
