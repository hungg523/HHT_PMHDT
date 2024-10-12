using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Interface;
using NhaThuoc.Domain.ReQuest.Coupon;

namespace NhaThuocOnline.WebApi.Controllers
{
    [Route("api/coupons")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService _couponService;
        public CouponsController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoupons()
        {
            var data = await _couponService.GetAllCouponList();
            return Ok(data);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CouponCreateRequest request)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _couponService.CreateCouponCode(request);
            if (result)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
