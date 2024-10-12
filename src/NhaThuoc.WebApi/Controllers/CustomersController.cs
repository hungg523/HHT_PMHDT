using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Interface;
using NhaThuoc.Domain.ReQuest.Customer;

namespace NhaThuocOnline.WebApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;
        public CustomersController(ICustomerService customerService, IConfiguration configuration) {
            _customerService = customerService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomerPaging();
            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var customer = await _customerService.GetCustomerById(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterRequest request)
        {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _customerService.Register(request);
                if (result)
                    return Ok(result);

                return BadRequest(result); 
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, CustomerUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _customerService.Update(customerId, request);
            if(result)
                return Ok(result);
            return BadRequest(result);

        }

        [HttpGet("{customerId}/addresses")]
        public async Task<IActionResult> GetCustomerAddress(int customerId)
        {
            var result= await _customerService.GetCustomerAddresses(customerId);
            return Ok(result);
        }

        [HttpPut("{customerId}/addresses")]
        public async Task<IActionResult> AddCustomerAddress(int customerId, [FromBody] CustomerAddressCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _customerService.CreateCustomerAddress(customerId, request);
            if (result)
                return Ok(result);
            return BadRequest(result);
        }


        [HttpPatch("{customerId}/softdelete")]
        public async Task<IActionResult> SoftDeleteCustomer(int customerId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _customerService.SoftDelete(customerId);
            if(result) 
                return Ok(result);
            return BadRequest();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _customerService.Authenticate(request);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
    }
}
    