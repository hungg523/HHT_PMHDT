﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.WebAdmin.Controller.Category
{
    public class AdminCategoryController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public AdminCategoryController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("/create-category")]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryRequest request)
        {
            try
            {
                var command = mapper.Map<CreateCategoryRequest>(request);
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpPut("/update-category")]
        public async Task<IActionResult> UpdateCategory(int? id, [FromBody] UpdateCategoryRequest request)
        {
            try
            {
                var command = mapper.Map<UpdateCategoryRequest>(request);
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