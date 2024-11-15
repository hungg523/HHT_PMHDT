﻿using MediatR;
using NhaThuoc.Application.DTOs;

namespace NhaThuoc.Application.Request.Order
{
    public class GetByIdOrderRequest : IRequest<OrderDTO>
    {
        public int? Id { get; set; }
    }
}
