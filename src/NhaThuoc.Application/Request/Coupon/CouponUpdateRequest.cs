﻿using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Coupon
{
    public class CouponUpdateRequest
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int? TimesUsed { get; set; }
        public int? MaxUsage { get; set; }
        public decimal? Discount { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponEndDate { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
