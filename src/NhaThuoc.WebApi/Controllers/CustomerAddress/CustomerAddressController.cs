using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Customers.CustomerAddress;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.WebApi.Controllers.CustomerAddress
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CustomerAddressController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        [HttpGet("/get-customeraddress-by-id")]
        public async Task<IActionResult> GetByIdCustomerAddress(int id)
        {
            try
            {
                var command = new GetByIdCustomerAddressRequest();
                command.Id = id;
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpGet("/get-customeraddresss")]
        public async Task<IActionResult> GetAllCustomerAddress()
        {
            try
            {
                var command = new GetAllCustomerAddressRequest();
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpPost("/create-customeraddress")]
        public async Task<IActionResult> CreateCustomerAddress([FromBody] CustomerAddressCreateRequest request)
        {
            try
            {
                var command = mapper.Map<CustomerAddressCreateRequest>(request);
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (NhaThuocException)
            {
                throw;
            }
        }

        [HttpPut("/update-customeraddress")]
        public async Task<IActionResult> UpdateCustomerAddress(int? id, [FromBody] CustomerAddressUpdateRequest request)
        {
            try
            {
                var command = mapper.Map<CustomerAddressUpdateRequest>(request);
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
