using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger, CancellationToken cancellationToken)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync(cancellationToken);
                logger.LogInformation("Seeded {context} with {count} records.", nameof(OrderContext), orderContext.Orders.Count());
            }
        }

        private static List<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
    {
        new Order
        {
            UserName = "arjun_singh",
            TotalPrice = 120.50m,
            FirstName = "arjun",
            LastName = "singh",
            EmailAddress = "arjun.singh@gmail.com",
            AddressLine = "123 Main Street",
            Country = "IND",
            State = "UP",
            ZipCode = "221011",
            CardName = "Arjun Singh",
            CardNumber = "4111111111111111",
            Expiration = "12/28",
            Cvv = "123",
            PaymentMethod = 1,
            CreatedBy = "Seed",
            CreatedAt = DateTime.UtcNow
        },
        new Order
        {
            UserName = "jane_smith",
            TotalPrice = 250.75m,
            FirstName = "Jane",
            LastName = "Smith",
            EmailAddress = "jane.smith@example.com",
            AddressLine = "456 Market Street",
            Country = "India",
            State = "Uttar Pradesh",
            ZipCode = "211001",
            CardName = "Jane Smith",
            CardNumber = "5555555555554444",
            Expiration = "10/27",
            Cvv = "456",
            PaymentMethod = 2,
            CreatedBy = "Seed",
            CreatedAt = DateTime.UtcNow
        },
        new Order
        {
            UserName = "alex_kumar",
            TotalPrice = 89.99m,
            FirstName = "Alex",
            LastName = "Kumar",
            EmailAddress = "alex.kumar@example.com",
            AddressLine = "789 MG Road",
            Country = "India",
            State = "Delhi",
            ZipCode = "110001",
            CardName = "Alex Kumar",
            CardNumber = "4007000000027",
            Expiration = "08/26",
            Cvv = "789",
            PaymentMethod = 1,
            CreatedBy = "Seed",
            CreatedAt = DateTime.UtcNow
        }
    };
        }
    }
}
