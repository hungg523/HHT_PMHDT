﻿using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.DependencyInjection.Extensions;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class GetCustomerByEmailRequestHandler : IRequestHandler<GetCustomerByEmailRequest, Entities.Customer>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public GetCustomerByEmailRequestHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<Entities.Customer> Handle(GetCustomerByEmailRequest request, CancellationToken cancellationToken)
        {
            var customers = await customerRepository.FindSingleAsync(x => x.Email == request.Email);
            if (customers is null) customers.ThrowNotFound();
            return mapper.Map<Entities.Customer>(customers);
        }
    }
}
