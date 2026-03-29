using Microsoft.Extensions.Logging;
using Ordering.Application.Abstractions;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Application.Mappers;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class UpdateOrderHandler : ICommandHandler<UpdateOrderCommand, int>
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<UpdateOrderHandler> _logger;

        public UpdateOrderHandler(IOrderRepository repository, ILogger<UpdateOrderHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<int> HandleAsync(UpdateOrderCommand command, CancellationToken cancellation)
        {
            var orderToUpdate = await _repository.GetByIdAsync(command.Id);
            if (orderToUpdate is null)
            {
                _logger.LogWarning($"Order ${command.FirstName} with Id:{command.Id} Not Found!");
                throw new NotFoundException(nameof(Order), command.Id);
            }

            orderToUpdate.ApplyToUpdate(command);
            await _repository.UpdateAsync(orderToUpdate);
            _logger.LogInformation($"Ordder With Id:{orderToUpdate.Id} is updated successfully");
            return orderToUpdate.Id;
        }
    }
}
