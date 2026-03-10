namespace Basket.Application.DTOs
{
    public record ShoppingCartDto(
        string UserName,
       List<ShoppingCardItemDto> Items,
       decimal TotalPrice
        );

    public record ShoppingCardItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set; }
    }

    public record CreateShoppingCartItemDto(
        string ProductId,
        string ProductName,
        string ImageFile,
        decimal Price,
        int Quentity
        );
}
