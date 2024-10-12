using Microsoft.AspNetCore.Identity.Data;
using NhaThuoc.Domain.ReQuest.Common;
using NhaThuoc.Domain.ReQuest.Customer;

namespace NhaThuoc.Application.Interface
{
    public interface ICustomerService
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
        Task<List<CustomerVm>> GetCustomerPaging();
        Task<CustomerVm> GetCustomerById(int id);
        Task<List<CustomerAddressVm>> GetCustomerAddresses(int customerId);
        Task<bool> Update(int id, CustomerUpdateRequest request);
        Task<bool> SoftDelete(int id);

        Task<bool> CreateCustomerAddress(int id,CustomerAddressCreateRequest request);

        //Task<ApiResult<bool>> CreateCustomerAddress(CustomerAddressCreateRequest request); 

        //Task<ApiResult<bool>> ChangePassword(ChangePasswordRequest request);
    }
}
