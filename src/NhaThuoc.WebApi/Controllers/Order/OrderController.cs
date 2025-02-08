using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Order;
using NhaThuoc.Domain.ReQuest.Order;

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
            var command = mapper.Map<CreateOrderRequest>(request);
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("/change-status-order")]
        public async Task<IActionResult> UpdateOrder(int? id, [FromBody] ChangeStatusOrderRequest request)
        {
            var command = mapper.Map<ChangeStatusOrderRequest>(request);
            command.Id = id;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/get-orders")]
        public async Task<IActionResult> GetAllOrder()
        {
            var command = new GetAllOrderRequest();
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/get-order-by-id")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var command = new GetByIdOrderRequest();
            command.Id = id;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/get-order-by-customer-id")]
        public async Task<IActionResult> GetOrderByCustomerId(int id)
        {
            var command = new GetOrderByCustomerIdRequest();
            command.Id = id;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/get-order-item-by-order-id")]
        public async Task<IActionResult> GetOrderItemByOrderId(int id)
        {
            var command = new GetOrderItemByOrderIdRequest();
            command.OrderId = id;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/order-statics")]
        public async Task<IActionResult> OrderStatics()
        {
            var command = new OrderStatisticsRequest();
            var result = mediator.Send(command);
            return Ok(result);
        }
    }
}