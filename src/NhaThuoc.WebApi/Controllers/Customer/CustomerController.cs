using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.WebApi.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CustomerController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet("/get-customer-roles")]
        public async Task<IActionResult> GetAllCustomerRoles()
        {
            try
            {
                var command = new GetAllCustomerRequest();
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpPut("/update-customer-profile")]
        public async Task<IActionResult> UpdateCustomerProfile(int? id, [FromBody] CustomerProfileDto request)
        {
            try
            {
                var command = mapper.Map<CustomerProfileDto>(request);
                command.Id = id;
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpPost("/customer-login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await mediator.Send(request);

                if (!response.IsSuccess)
                {
                    return StatusCode(response.StatusCode, response);
                }

                return Ok(response);
            }
            catch (NhaThuocException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Đã có lỗi xảy ra khi đăng nhập." });
            }
        }

        [HttpPost("/customer-register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var command = mapper.Map<RegisterRequest>(request);
                var response = await mediator.Send(command);
                return Ok(response);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpPut("/authen-customer")]
        public async Task<IActionResult> AuthenCustomer(int? id,[FromBody] AurhenCustomerRequest request)
        {
            try
            {
                var command = mapper.Map<AurhenCustomerRequest>(request);
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
