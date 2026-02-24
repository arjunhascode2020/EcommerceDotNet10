using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
        {
            var query = new GetBasketByUserNameQuery(userName);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCartResponse>> CreateOrUpdateBasket(CreateShoppingCartCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpDelete("{userName}")]
        public async Task<ActionResult<Response>> DeleteBasket(string userName)
        {
            var query = new DeleteBasketByUserCommand(userName);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
