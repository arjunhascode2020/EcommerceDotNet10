using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Abstractions;
using Ordering.Application.Commands;
using Ordering.Application.DTOs;
using Ordering.Application.Mappers;
using Ordering.Application.Queries;
namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class OrdersController : ControllerBase
    {
        private readonly ICommandHandler<CreateOrderCommand, int> _createOrderHandler;
        private readonly ICommandHandler<UpdateOrderCommand, int> _updateOrderHandler;
        private readonly ICommandHandler<DeleteOrderCommand> _deleteOrderHandler;
        private readonly IQueryHandler<GetOrderListQuery, List<OrderDto>> _getOrderListHandler;
        private readonly ILogger<OrdersController> _logger;



        public OrdersController(
            ICommandHandler<CreateOrderCommand, int> createOrderHandler,
            ICommandHandler<UpdateOrderCommand, int> updateOrderHandler,
            ICommandHandler<DeleteOrderCommand> deleteOrderHandler,
            IQueryHandler<GetOrderListQuery, List<OrderDto>> getOrderListHandler,
            ILogger<OrdersController> logger
            )
        {
            _createOrderHandler = createOrderHandler;
            _updateOrderHandler = updateOrderHandler;
            _deleteOrderHandler = deleteOrderHandler;
            _getOrderListHandler = getOrderListHandler;
            _logger = logger;
        }

        [HttpGet("{userName}", Name = "GetOrdersByUserName")]
        public async Task<ActionResult<List<OrderDto>>> GetOrdersByUserName(string userName, CancellationToken cancellationToken)
        {
            var query = new GetOrderListQuery(userName);
            var orders = await _getOrderListHandler.HandleAsync(query, cancellationToken);

            _logger.LogInformation("Retrieved {count} orders for user {userName}", orders.Count, userName);
            return Ok(orders);

        }

        [HttpPost(Name = "CheckoutOrder")]
        public async Task<IActionResult> CheckoutOrder([FromBody] CreateOrderDto createOrderDto, CancellationToken cancellationToken)
        {
            var command = createOrderDto.ToCommand();
            var orderId = await _createOrderHandler.HandleAsync(command, cancellationToken);
            _logger.LogInformation("Created order for user {userName} & Id :{id}", createOrderDto.UserName, orderId);
            return Ok(orderId);

        }

        [HttpPut("{id}", Name = "UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDto updateOrderDto, CancellationToken cancellationToken)
        {
            var command = updateOrderDto.ToCommand(id);
            var updateOrderId = await _updateOrderHandler.HandleAsync(command, cancellationToken);
            _logger.LogInformation("Updated order with id {id} for user {userName}", id, updateOrderDto.UserName);
            return NoContent();
        }
        [HttpDelete("{id}", Name = "DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteOrderCommand(id);
            await _deleteOrderHandler.HandleAsync(command, cancellationToken);
            _logger.LogInformation("Deleted order with id {id}", id);
            return NoContent();
        }
    }
}
