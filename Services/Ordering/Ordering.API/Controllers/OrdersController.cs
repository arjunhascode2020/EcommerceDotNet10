using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.DTOs;
using Ordering.Application.Handlers;
using Ordering.Application.Mappers;
using Ordering.Application.Queries;
namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderHandler _createOrderHandler;
        private readonly UpdateOrderHandler _updateOrderHandler;
        private readonly GetOrderListHandler _getOrderListHandler;
        private readonly DeleteOrderHandler _deleteOrderHandler;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(
            CreateOrderHandler createOrderHandler,
            UpdateOrderHandler updateOrderHandler,
            GetOrderListHandler getOrderListHandler,
            DeleteOrderHandler deleteOrderHandler,
            ILogger<OrdersController> logger
            )
        {
            _createOrderHandler = createOrderHandler;
            _updateOrderHandler = updateOrderHandler;
            _getOrderListHandler = getOrderListHandler;
            _deleteOrderHandler = deleteOrderHandler;
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
