using Microsoft.AspNetCore.Http;

namespace NhaThuoc.Application.Exceptions
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }

        public static ApiResponse Success()
        {
            return new ApiResponse
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}