using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Interface;
using NhaThuoc.Domain.ReQuest.Batch;
using NhaThuoc.Domain.ReQuest.Product;

namespace NhaThuocOnline.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _productService.GetAllProductsPaging(request);
            return Ok(products);
        }

        [HttpGet("public")]
        public async Task<IActionResult> GetProductsByCategoryId([FromQuery] GetPublicProductPagingRequest request)
        {
            var products = await _productService.GetAllByCategoryIdPaging(request);
            return Ok(products);
        }


        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productService.GetProductById(productId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newProductId = await _productService.Create(request);
            if (newProductId == 0)
               return BadRequest();
            var product= await _productService.GetProductById(newProductId);
            return CreatedAtAction(nameof(GetProductById), new { productId= newProductId}, product);
        }

        [HttpPut("{id}/categories")]
        public async Task<IActionResult> CategoryAssign(int id, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.CategoryAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("{productId}/softdelete")]
        public async Task<IActionResult> SoftDeleteProduct(int productId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productService.SoftDelete(productId);
            if (result)
                return Ok(result);
            return BadRequest();
        }

        [HttpGet("{productId}/batches")]
        public async Task<IActionResult> GetBatchByProductId(int productId)
        {
            var data = await _productService.GetAllBatch(productId);
            return Ok(data);
        }


        [HttpPost("{productId}/batches")]
        public async Task<IActionResult> Create([FromForm] BatchCreateRequest request)
        {
            var data = await _productService.CreateBatch(request);
            return Ok(data);
        }

        [HttpPatch("{productId}/stock")]
        public async Task<IActionResult> UpdateStock([FromForm] BatchUpdateStockRequest request)
        {
            var data = await _productService.UpdateStock(request);
            return Ok(data);
        }
    }
}
