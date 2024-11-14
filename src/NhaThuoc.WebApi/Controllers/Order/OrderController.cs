using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Order;
using NhaThuoc.Domain.ReQuest.Order;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.WebApi.Controllers.Order
{
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public OrderController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("/create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var command = mapper.Map<CreateOrderRequest>(request);
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpPut("/change-status-order")]
        public async Task<IActionResult> UpdateOrder(int? id, [FromBody] ChangeStatusOrderRequest request)
        {
            try
            {
                var command = mapper.Map<ChangeStatusOrderRequest>(request);
                command.Id = id;
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpGet("/get-orders")]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                var command = new GetAllOrderRequest();
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpGet("/get-order-by-id")]
        public async Task<IActionResult> GetByIdOrder(int id)
        {
            try
            {
                var command = new GetByIdOrderRequest();
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