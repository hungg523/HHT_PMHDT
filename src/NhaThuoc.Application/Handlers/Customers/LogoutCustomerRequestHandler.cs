using MediatR;
using Microsoft.AspNetCore.Http;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class LogoutCustomerRequestHandler : IRequestHandler<LogoutCustomerRequest, ApiResponse>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public LogoutCustomerRequestHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse> Handle(LogoutCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                httpContextAccessor.HttpContext.Session.Clear();
                httpContextAccessor.HttpContext.Response.Cookies.Delete(".AspNetCore.Session");
                return ApiResponse.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}