namespace Ordering.Application.DTOs
{
    public record OrderDto(
        int Id,
        string UserName,
        decimal? TotalPrice,
        string FirstName,
        string LastName,
        string EmailAddress,
        string AddressLine,
        string Country,
        string State,
        string ZipCode,
        string CardName,
        string CardNumber,
        string Expiration,
        string Cvv,
        int? PaymentMethod,
        DateTime CreatedAt,
        string CreatedBy,
        DateTime? LastModifyAt,
        string? LastModifyBy
        );
    public record CreateOrderDto(
            string UserName,
            decimal TOtalPrice,
            string FirstName,
            string LastName,
            string EmailAddress,
            string AddressLine,
            string Country,
            string State,
            string ZipCode,
            string CardName,
            string CardNumber,
            string Expiration,
            string Cvv,
            int PaymentMethod
        );
    public record UpdateOrderDto(
           string UserName,
           decimal TOtalPrice,
           string FirstName,
           string LastName,
           string EmailAddress,
           string AddressLine,
           string Country,
           string State,
           string ZipCode,
           string CardName,
           string CardNumber,
           string Expiration,
           string Cvv,
           int PaymentMethod
       );

}
