using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace NhaThuoc.Share.Exceptions
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StageTrace { get; set; }

        public static ApiResponse Success(string stageTrace = null)
        {
            return new ApiResponse
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                StageTrace = stageTrace
            };
        }
    }
}