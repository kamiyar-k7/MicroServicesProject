using DiscountApi.Entities;
using DiscountApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiscountApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {

        #region Ctor

        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        #endregion

        #region Get Coupon

        [HttpGet("[action]/{productName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {

            var coupon = await _discountRepository.GetDiscount(productName);
            return Ok(coupon);

        }

        #endregion

        #region create Coupon

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<bool>> CreateDiscount(Coupon coupon)
        {
            await _discountRepository.CreateDiscount(coupon);

            return Ok(true);

        }

        #endregion

        #region Update Coupon

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> UpdateDiscount(Coupon coupon)
        {
        

            return Ok(await _discountRepository.UpdateDiscount(coupon));

        }

        #endregion

        #region Update Coupon

        [HttpDelete("[action]/{productName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> UpdateDiscount(string productName)
        {


            return Ok(await _discountRepository.DeleteDiscount(productName));

        }

        #endregion
    }
}
