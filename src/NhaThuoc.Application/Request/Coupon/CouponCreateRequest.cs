using MediatR;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Coupon
{
    public class CouponCreateRequest : IRequest<ApiResponse>
    {
        public string? Code { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public int? TimesUsed { get; set; } = 0;
        public int? MaxUsage { get; set; }
        public string? Discount { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponEndDate { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
