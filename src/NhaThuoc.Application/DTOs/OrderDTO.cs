﻿using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.Enums;

namespace NhaThuoc.Application.DTOs
{
    public class OrderDTO
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        public OrderStatus? Status { get; set; }
        public decimal? TotalPrice { get; set; }
        public CustomerAddressDTO? Address { get; set; }
        public CouponDTO? Coupon { get; set; }
        public ICollection<OrderItemDTO>? OrderItems { get; set; }
    }
}