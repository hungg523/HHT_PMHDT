﻿using MediatR;
using NhaThuoc.Application.DTOs;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class TopCustomersRequest : IRequest<List<TopCustomerDTO>>
    {
    }
}