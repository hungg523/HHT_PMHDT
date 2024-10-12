using Microsoft.AspNetCore.Mvc;

namespace NhaThuoc.WebApi.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> Details(string cartId) {
            var result = await _cartService.GetByCartId(cartId);
            return Ok(result);
        }

        [HttpGet("{customerId}/recently")]
        public async Task<IActionResult> GetCartIdRecently(int customerId)
        {
            var result = await _cartService.GetCartIdRecently(customerId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CartCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCartId = await _cartService.CreateCartItem(request);
            if (string.IsNullOrEmpty(newCartId))
                return BadRequest();
            var cartItem = await _cartService.GetByCartId(newCartId);
            return CreatedAtAction(nameof(Details), new { cartId = newCartId }, cartItem);

          
        }

        [HttpPatch("quantity")]
        public async Task<IActionResult> UpdateQuantityProduct([FromForm]CartUpdateQuantityRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data = await _cartService.UpdateQuantity(request);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data= await _cartService.DeleteItem(id);
            return Ok(data);
        }

       
    }
}
