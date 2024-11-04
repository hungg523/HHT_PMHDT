using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.WebAdmin.Controller.Product
{
    public class AdminProductController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public AdminProductController(IMediator mediator, IMapper mapper)
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
    }
}