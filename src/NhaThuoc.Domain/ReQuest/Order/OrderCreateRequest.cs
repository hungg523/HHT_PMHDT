﻿using NhaThuoc.Domain.Entities;
using NhaThuoc.Domain.Enums;

namespace NhaThuoc.Domain.ReQuest.Order
{
    public class OrderCreateRequest
    {
        public int CouponId { get; set; }
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }

}
