using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Domain.Exceptions;

namespace NhaThuoc.WebAdmin.Controller.Category
{
    public class CreateCategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public CreateCategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("/create-category")]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryRequest request)
        {
            try
            {
                var command = new CreateCategoryRequest()
                {
                    Name = request.Name,
                    ParentId = request.ParentId,
                    Description = request.Description,
                    ImagePath = request.ImagePath,
                    IsActive = request.IsActive,
                };
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