using Ordering.Application.Commands;
using Ordering.Application.DTOs;
using Ordering.Core.Entities;

namespace Ordering.Application.Mappers
{
    public static class OrderMapper
    {

        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto(
                   order.Id,
                   order.UserName!,
                   order.TotalPrice ?? 0,
                   order.FirstName!,
                   order.LastName!,
                   order.EmailAddress!,
                   order.AddressLine!,
                   order.Country!,
                   order.State!,
                   order.ZipCode!,
                   order.CardName!,
                   order.CardNumber!,
                   order.Expiration!,
                   order.Cvv!,
                   order.PaymentMethod ?? 0,
                   order.CreatedAt,
                   order.CreatedBy,
                   order.LastUpdateAt,
                   order.LastModifyBy
                );
        }

        public static Order ToEntity(this CreateOrderCommand command)
        {
            return new Order
            {
                UserName = command.UserName,
                TotalPrice = command.TotalPrice,
                FirstName = command.FirstName,
                LastName = command.LastName,
                EmailAddress = command.EmailAddress,
                AddressLine = command.AddressLine,
                Country = command.Country,
                State = command.State,
                ZipCode = command.ZipCode,
                CardName = command.CardName,
                CardNumber = command.CardNumber,
                Expiration = command.Expiration,
                Cvv = command.Cvv,
                PaymentMethod = command.PaymentMethod,
            };
        }

        public static void ApplyToUpdate(this Order order, UpdateOrderCommand command)
        {
            order.UserName = command.UserName;
            order.TotalPrice = command.TotalPrice;
            order.FirstName = command.FirstName;
            order.LastName = command.LastName;
            order.EmailAddress = command.EmailAddress;
            order.AddressLine = command.AddressLine;
            order.Country = command.Country;
            order.State = command.State;
            order.ZipCode = command.ZipCode;
            order.CardName = command.CardName;
            order.CardNumber = command.CardNumber;
            order.Expiration = command.Expiration;
            order.Cvv = command.Cvv;
            order.PaymentMethod = command.PaymentMethod;
        }

        public static CreateOrderCommand ToCommand(this CreateOrderDto createOrderDto)
        {
            return new CreateOrderCommand
            {
                UserName = createOrderDto.UserName,
                FirstName = createOrderDto.FirstName,
                LastName = createOrderDto.LastName,
                TotalPrice = createOrderDto.TOtalPrice,
                EmailAddress = createOrderDto.EmailAddress,
                AddressLine = createOrderDto.AddressLine,
                Country = createOrderDto.Country,
                State = createOrderDto.State,
                ZipCode = createOrderDto.ZipCode,
                CardName = createOrderDto.CardName,
                CardNumber = createOrderDto.CardNumber,
                Expiration = createOrderDto.Expiration,
                Cvv = createOrderDto.Cvv,
                PaymentMethod = createOrderDto.PaymentMethod,
            };
        }
        public static UpdateOrderCommand ToCommand(this UpdateOrderDto updateOrderDto, int id)
        {
            return new UpdateOrderCommand
            {
                Id = id,
                UserName = updateOrderDto.UserName,
                FirstName = updateOrderDto.FirstName,
                LastName = updateOrderDto.LastName,
                TotalPrice = updateOrderDto.TOtalPrice,
                EmailAddress = updateOrderDto.EmailAddress,
                AddressLine = updateOrderDto.AddressLine,
                Country = updateOrderDto.Country,
                State = updateOrderDto.State,
                ZipCode = updateOrderDto.ZipCode,
                CardName = updateOrderDto.CardName,
                CardNumber = updateOrderDto.CardNumber,
                Expiration = updateOrderDto.Expiration,
                Cvv = updateOrderDto.Cvv,
                PaymentMethod = updateOrderDto.PaymentMethod,
            };
        }
    }
}
