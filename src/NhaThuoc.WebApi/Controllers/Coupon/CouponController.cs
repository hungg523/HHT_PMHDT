using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Application.Request.Coupon;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.WebApi.Controllers.Coupon
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IMediator mediator;

        public CouponController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("/get-coupon-name")]
        public async Task<IActionResult> GetByNameCoupon(int id)
        {
            try
            {
                var command = new GetByNameCouponRequest();
                command.Id = id;
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpGet("/get-coupons")]
        public async Task<IActionResult> GetAllCoupon()
        {
            try
            {
                var command = new GetAllCouponRequest();
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }
    }
}
