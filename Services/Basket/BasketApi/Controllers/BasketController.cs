
using BasketApi.Entities;
using BasketApi.GRPC_Services;
using BasketApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{

    #region Ctor
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;
    public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
    {
        _basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
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



}
