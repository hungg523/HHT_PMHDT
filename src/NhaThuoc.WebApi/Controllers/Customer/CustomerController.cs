using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Customers.Customer;

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


        [HttpGet("/get-customer-by-email")]
        public async Task<IActionResult> GetCustomerByEmail(string email)
        {
            var command = new GetCustomerByEmailRequest();
            command.Email = email;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/get-customer-by-id")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var command = new GetCustomerByIdCustomerRequest();
            command.Id = id;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/get-roles-customer")]
        public async Task<IActionResult> GetAllCustomerRoles()
        {
            var command = new GetAllCustomerRequest();
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("/update-profile-customer")]
        public async Task<IActionResult> UpdateCustomerProfile(int? id, [FromBody] UpdateProifleCustomerRequest request)
        {
            var command = mapper.Map<UpdateProifleCustomerRequest>(request);
            command.Id = id;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("/login-customer")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = mapper.Map<LoginRequest>(request);
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("/register-customer")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var command = mapper.Map<RegisterRequest>(request);
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("/authen-customer")]
        public async Task<IActionResult> AuthenCustomer(string? email, [FromBody] AuthenCustomerRequest request)
        {
            var command = mapper.Map<AuthenCustomerRequest>(request);
            command.Email = email;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("/change-password")]
        public async Task<IActionResult> ChangePassword(string? email)
        {
            var command = new ChangePasswordRequest();
            command.Email = email;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("/update-customer-password")]
        public async Task<IActionResult> UpdateCustomerPassword(string? email, [FromBody] UpdateCustomerPasswordRequest request)
        {
            var command = mapper.Map<UpdateCustomerPasswordRequest>(request);
            command.Email = email;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("/resend-otp")]
        public async Task<IActionResult> ResendOTP(string? email, [FromBody] ResendOTPRequest request)
        {
            var command = mapper.Map<ResendOTPRequest>(request);
            command.Email = email;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("/customer-logout")]
        public async Task<IActionResult> Logout(LogoutCustomerRequest request)
        {
            var command = mapper.Map<LogoutCustomerRequest>(request);
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/top-customer")]
        public async Task<IActionResult> TopCustomer()
        {
            var command = new TopCustomersRequest();
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/dashboard-statistics")]
        public async Task<IActionResult> DashboardStatistics()
        {
            var command = new DashboardStatisticsRequest();
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}