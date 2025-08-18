using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingApplication.Features.Orders.Commands.CheckoutOrder;
using OrderingApplication.Features.Orders.Commands.DeleteOrder;
using OrderingApplication.Features.Orders.Commands.UpdateOrder;
using OrderingApplication.Features.Orders.Queries.GetOrdersList;
using System.Net;

namespace OrderingApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        #region Ctor

        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Get All Orders

        [HttpGet("[action]/{userName}")]
        [ProducesResponseType(typeof(IEnumerable<OrdersViewModel>) , (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersViewModel>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrdersListQueryRequest(userName);
            var orders = await _mediator.Send(query);

            return Ok(orders);

        }

        #endregion

        #region CheckOut order (addorder)
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckOutOrder([FromBody] CheckoutOrderCommandRequest command)
        {
            var result =await _mediator.Send(command);
            return Ok(result);
        }

        #endregion

        #region Update order
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public async Task<IActionResult> UpdateOrder(UpdateOrderCommandRequest command)
        {
             await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region Delete order

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _mediator.Send(new DeleteOrderCommandRequest { Id = id} );
            return NoContent();
        }

        #endregion
    }
}
