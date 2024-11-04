using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Coupon;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.WebAdmin.Controller.Coupon
{
    public class AdminCouponController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public AdminCouponController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("/create-coupon")]
        public async Task<IActionResult> CreateCoupon([FromBody] CouponCreateRequest request)
        {
            try
            {
                var command = mapper.Map<CouponCreateRequest>(request);
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpPut("/update-coupon")]
        public async Task<IActionResult> UpdateCoupon(int? id, [FromBody] CouponUpdateRequest request)
        {
            try
            {
                var command = mapper.Map<CouponUpdateRequest>(request);
                command.Id = id;
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