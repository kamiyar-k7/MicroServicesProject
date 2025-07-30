
using BasketApi.Entities;
using BasketApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{

    #region Ctor
    private readonly IBasketRepository _basketRepository;
    public BasketController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }
    #endregion


    #region General

    [HttpGet("[action]/userName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ShoppingCart>> GetUserbasket(string userName)
    {
        
        var basket = await _basketRepository.GetUserBasket(userName);

        return Ok(basket ?? new ShoppingCart());
        
    }


    [HttpPost("[action]/basket")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ShoppingCart>> GetUserbasket(ShoppingCart basket)
    {

        return Ok(await _basketRepository.UpdateBasket(basket));    
    }


    [HttpDelete("[action]/userName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Deletebasket(string userName)
    {

        await _basketRepository.DeleteBasket(userName);
        return Ok();    
    }
    #endregion

}
