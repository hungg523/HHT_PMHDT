using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Share.Exceptions;
using System.Drawing.Printing;

namespace NhaThuoc.WebApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("/create-product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            try
            {
                var command = mapper.Map<CreateProductRequest>(request);
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpPut("/update-product")]
        public async Task<IActionResult> UpdateProuct(int? id, [FromBody] UpdateProductRequest request)
        {
            try
            {
                var command = mapper.Map<UpdateProductRequest>(request);
                command.Id = id;
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpGet("/get-name-product")]
        public async Task<IActionResult> GetByNameProduct(string productname, int? pageNumber, int? pageSize)
        {
            try
            {
                var command = new GetByNameProductRequest();
                command.ProductName = productname;
                command.PageNumber = pageNumber ?? 1;
                command.PageSize = pageSize ?? 6;
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpGet("/get-products")]
        public async Task<IActionResult> GetAllProduct(int? pageNumber, int? pageSize)
        {
            try
            {
                var command = new GetAllProductRequest();
                command.PageNumber = pageNumber ?? 1;
                command.PageSize = pageSize ?? 6;
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpGet("/get-products-admin")]
        public async Task<IActionResult> GetAllProductAdmin()
        {
            try
            {
                var command = new GetAllProductsRequest();
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpGet("/get-products-by-category")]
        public async Task<IActionResult> GetAllProductByCategory(int? id, int? pageNumber, int? pageSize)
        {
            try
            {
                var command = new GetProductByCategoryIdRequest();
                command.CategoryId = id;
                command.PageNumber = pageNumber ?? 1;
                command.PageSize = pageSize ?? 6;
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpGet("/get-product-detail")]
        public async Task<IActionResult> GetProductDetail(int? id)
        {
            try
            {
                var command = new GetProductDeatilRequest();
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
