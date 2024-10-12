using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Interface;
using NhaThuoc.Domain.ReQuest.Order;

namespace NhaThuocOnline.WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public OrdersController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.CreateOrder(request);
            return Ok();
        }

        [HttpGet("checkout")]
        public async Task<IActionResult> GetProductInCart(string cartId)
        {  
            var result = await _orderService.GetProductByCartId(cartId);
            return Ok(result);
        }

        [HttpPatch("status")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.ChangeStatusOrder(request);
            return Ok(result);

        }
    }
}
