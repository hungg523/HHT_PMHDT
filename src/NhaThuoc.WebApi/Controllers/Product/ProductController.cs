using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.WebApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("/get-product-name")]
        public async Task<IActionResult> GetByNameProduct(int id)
        {
            try
            {
                var command = new GetByNameProductRequest();
                command.Id = id;
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpGet("/get-products")]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var command = new GetAllProductRequest();
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpGet("/get-products-by category")]
        public async Task<IActionResult> GetAllProductByCategory(int id)
        {
            try
            {
                var command = new GetProductByCategoryIdRequest();
                command.CategoryId = id;
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
