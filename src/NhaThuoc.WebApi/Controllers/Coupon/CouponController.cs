﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Handlers.Coupon;
using NhaThuoc.Application.Request.Coupon;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.WebApi.Controllers.Coupon
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CouponController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet("/get-coupon-by-id")]
        public async Task<IActionResult> GetByIdCoupon(int id)
        {
            var command = new GetByIdCouponRequest();
            command.Id = id;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/get-coupons")]
        public async Task<IActionResult> GetAllCoupon()
        {
            var command = new GetAllCouponRequest();
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("/create-coupon")]
        public async Task<IActionResult> CreateCoupon([FromBody] CouponCreateRequest request)
        {
            var command = mapper.Map<CouponCreateRequest>(request);
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("/update-coupon")]
        public async Task<IActionResult> UpdateCoupon(int? id, [FromBody] CouponUpdateRequest request)
        {
            var command = mapper.Map<CouponUpdateRequest>(request);
            command.Id = id;
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
