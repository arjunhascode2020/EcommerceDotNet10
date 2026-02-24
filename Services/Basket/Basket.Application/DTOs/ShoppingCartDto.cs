namespace Basket.Application.DTOs
{
    public record ShoppingCartDto(
        string UserName,
       List<ShoppingCardItemDto> Items,
       decimal TotalPrice
        );

    public record ShoppingCardItemDto(
        string ProductId,
        string ProductName,
        string ImageFile,
        decimal Price,
        int Quentity
        );

    public record CreateShoppingCartItemDto(
        string ProductId,
        string ProductName,
        string ImageFile,
        decimal Price,
        int Quentity
        );
}
