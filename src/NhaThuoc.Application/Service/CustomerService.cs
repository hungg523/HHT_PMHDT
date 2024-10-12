using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NhaThuoc.Application.Interface;
using NhaThuoc.Domain.Data;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Domain.ReQuest.Common;
using NhaThuoc.Domain.ReQuest.Customer;
using NNhaThuoc.Domain.ReQuest.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NhaThuoc.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public CustomerService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            //var user = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Email == request.Email && x.PasswordHash == request.Password);
            //if (user == null)
            //    return new ApiErrorResult<string>("Sai tên đăng nhập hoặc mật khẩu");

            var user = await _dbContext.Customers
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user == null || user.PasswordHash != request.Password)
                return new ApiErrorResult<string>("Sai tên đăng nhập hoặc mật khẩu");


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(3); 

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                  new Claim(ClaimTypes.Email, user.Email),
        }),
                Expires = expires,
                SigningCredentials = creds,
                Issuer = _configuration["Tokens:Issuer"],
                Audience = _configuration["Tokens:Issuer"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new ApiSuccessResult<string>(tokenHandler.WriteToken(token));
        }






        public async Task<bool> SoftDelete(int id)
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Id == id);
            if (customer != null)
            {

                customer.IsActive = false;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CustomerVm> GetCustomerById(int id)
        {

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer != null)
            {
                var result = new CustomerVm()
                {
                    Id = id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    IsActive = customer.IsActive

                };
                return result;
            }
            return new CustomerVm();

        }

        public async Task<List<CustomerVm>> GetCustomerPaging()
        {
            var customersVm = await _dbContext.Customers.Select(x => new CustomerVm
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                IsActive = x.IsActive

            }).ToListAsync();
            return new List<CustomerVm>(customersVm);
        }
        public async Task<bool> Update(int id, CustomerUpdateRequest request)
        {
            var hasCustomer = await _dbContext.Customers.AnyAsync(x => x.Email == request.Email && x.Id == id);
            if (!hasCustomer)
            {
                return false;
            }

            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer != null)
            {
                customer.Id = id;
                customer.Email = request.Email;
                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.PhoneNumber = request.PhoneNumber;
                customer.AvatarImagePath = request.AvatarImagePath;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == request.Email);
            if (existingCustomer != null)
            {
                return false; // Khách hàng đã tồn tại
            }

            if (request.Password != request.ConfirmPassword)
            {
                return false; // Mật khẩu và xác nhận mật khẩu không khớp
            }

            var passwordHash = HashPassword(request.Password);

            var customerAccount = new Customer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                AvatarImagePath = "/img/default-avt",
                PhoneNumber = "chưa thêm",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Customers.Add(customerAccount);
            await _dbContext.SaveChangesAsync();

            return true; // Đăng ký thành công
        }

        public async Task<bool> CreateCustomerAddress(int id, CustomerAddressCreateRequest request)
        {
            var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);
            if (existingCustomer == null)
            {
                return false;
            }
            var customerAddress = new CustomerAddress()
            {
                CustomerId = request.CustomerId,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                City = request.City,
                District = request.District,
                ReceiverName = request.ReceiverName,
                ReceiverPhone = request.ReceiverPhone,
            };
            
             _dbContext.CustomerAddresses.Add(customerAddress);
            await _dbContext.SaveChangesAsync();

            return true;


        }

        private string HashPassword(string password)
        {
            using (SHA256 hash = SHA256.Create())
            {
                var pwdBytes = Encoding.UTF8.GetBytes(password);
                var hashedPwd = hash.ComputeHash(pwdBytes);
                return Convert.ToHexString(hashedPwd);
            }
        }

        public async Task<List<CustomerAddressVm>> GetCustomerAddresses(int customerId)
        {
            var customerAddressEntities = await _dbContext.CustomerAddresses
                                              .Where(x => x.CustomerId == customerId)
                                              .ToListAsync();

            var customerAddressViewModels = customerAddressEntities
                .Select(addressEntity => new CustomerAddressVm
                {
                    CustomerId = addressEntity.CustomerId,
                    Addresses = customerAddressEntities, 
                })
                .ToList();

            return customerAddressViewModels;
        }

    }
}
