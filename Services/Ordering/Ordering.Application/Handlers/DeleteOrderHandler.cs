using Microsoft.Extensions.Logging;
using Ordering.Application.Abstractions;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class DeleteOrderHandler : ICommandHandler<DeleteOrderCommand>
    {
        private readonly ILogger<DeleteOrderHandler> _logger;
        private readonly IOrderRepository _repository;

        public DeleteOrderHandler(ILogger<DeleteOrderHandler> logger, IOrderRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task HandleAsync(DeleteOrderCommand command, CancellationToken cancellation)
        {
            var order = await _repository.GetByIdAsync(command.Id);
            if (order is null)
            {
                _logger.LogWarning("Order with id {Id} not found", command.Id);
                return;
            }
            await _repository.DeleteAsync(order);
            _logger.LogInformation("Order with id {Id} deleted successfully", command.Id);
        }
    }
}
