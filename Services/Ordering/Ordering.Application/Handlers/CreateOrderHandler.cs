using Ordering.Application.Abstractions;
using Ordering.Application.Commands;
using Ordering.Application.Mappers;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<int> HandleAsync(CreateOrderCommand command, CancellationToken cancellation)
        {
            var orderEntity = command.ToEntity();
            var generatedOrder = await _orderRepository.AddAsync(orderEntity);

            return generatedOrder.Id;
        }
    }
}
