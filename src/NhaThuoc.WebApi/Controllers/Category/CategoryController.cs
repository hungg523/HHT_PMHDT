using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.WebApi.Controllers.Category
{

    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CategoryController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("/create-category")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var command = mapper.Map<CreateCategoryRequest>(request);
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("/update-category")]
        public async Task<IActionResult> UpdateCategory(int? id, [FromBody] UpdateCategoryRequest request)
        {
            var command = mapper.Map<UpdateCategoryRequest>(request);
            command.Id = id;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("/get-category-by-id")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var command = new GetByIdCategoryRequest();
            command.Id = id;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/get-categories")]
        public async Task<IActionResult> GetAllCategory()
        {
            var command = new GetAllCategoryRequest();
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
