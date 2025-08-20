
using AutoMapper;
using BasketApi.Entities;
using BasketApi.GRPC_Services;
using BasketApi.Repositories;
using EventBusMessages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BasketApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{

    #region Ctor
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }
    #endregion

    #region Get User Basket

    [HttpGet("[action]/{userName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ShoppingCart>> GetUserbasket(string userName)
    {

        var basket = await _basketRepository.GetUserBasket(userName);

        return Ok(basket ?? new ShoppingCart(userName));

    }
    #endregion

    #region Update Basket


    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
     

        foreach (var item in basket.Items)
        {
            var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            item.Price -= coupon.Amount;
        }

        return Ok(await _basketRepository.UpdateBasket(basket));
    }
    #endregion

    #region Delete BAsket


    [HttpDelete("[action]/{userName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Deletebasket(string userName)
    {

        await _basketRepository.DeleteBasket(userName);
        return Ok();
    }
    #endregion


    #region CheckOut

    [HttpPost("[action]")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout(BasketCheckout basketCheckout)
    {
        //get existing basket with total price
        var basket = await _basketRepository.GetUserBasket(basketCheckout.UserName);
        if (basket == null)
        {
            return BadRequest();
        }

        // create bacsketchekoutevent -- set total price on basketchekout event message
        var eventmessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventmessage.TotalPrice = basket.TotalPrice;


        // send checkout event to rabbitmq 
        await _publishEndpoint.Publish(eventmessage);


        //remove basket
        await _basketRepository.DeleteBasket(basketCheckout.UserName);


        return Ok();
    }

    #endregion
}
