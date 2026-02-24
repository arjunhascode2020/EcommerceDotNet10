using Basket.Application.Commands;
using Basket.Application.DTOs;
using Basket.Application.Responses;
using Basket.Core.Entities;

namespace Basket.Application.Mappers
{
    public static class BasketMapper
    {

        public static ShoppingCartItemResponse ToResponse(this ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem == null)
            {
                return new ShoppingCartItemResponse();
            }
            return new ShoppingCartItemResponse
            {
                ProductId = shoppingCartItem.ProductId,
                ProductName = shoppingCartItem.ProductName,
                Price = shoppingCartItem.Price,
                ImageFile = shoppingCartItem.ImageFile,
                Quentity = shoppingCartItem.Quentity
            };
        }
        public static ShoppingCartResponse ToResponse(this ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                return new ShoppingCartResponse();
            }
            var items = shoppingCart.Items.Select(item => item.ToResponse()).ToList();
            return new ShoppingCartResponse(shoppingCart.UserName, items);
        }

        public static ShoppingCartItem ToEntity(this ShoppingCardItemDto shoppingCardItemDto)
        {
            if (shoppingCardItemDto == null)
            {
                return new ShoppingCartItem();
            }
            return new ShoppingCartItem
            {
                ProductId = shoppingCardItemDto.ProductId,
                ProductName = shoppingCardItemDto.ProductName,
                Price = shoppingCardItemDto.Price,
                ImageFile = shoppingCardItemDto.ImageFile,
                Quentity = shoppingCardItemDto.Quentity
            };
        }
        public static ShoppingCart ToEntity(this CreateShoppingCartCommand command)
        {
            if (command == null)
            {
                return new ShoppingCart();
            }
            var items = command.items.Select(item => item.ToEntity()).ToList();
            return new ShoppingCart
            {
                UserName = command.userName,
                Items = items
            };
        }
    }
}
