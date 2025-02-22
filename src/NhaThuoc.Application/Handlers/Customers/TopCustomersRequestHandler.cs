using MediatR;
using Microsoft.EntityFrameworkCore;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class TopCustomersRequestHandler : IRequestHandler<TopCustomersRequest, List<TopCustomerDTO>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;

        public TopCustomersRequestHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<List<TopCustomerDTO>> Handle(TopCustomersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var groupedOrders = orderRepository.FindAll().GroupBy(o => o.CustomerId).ToList();
                var customerIds = groupedOrders.Select(g => (int)g.Key).ToList();
                var customers = await customerRepository.FindAll(x => customerIds.Contains(x.Id)).ToListAsync(cancellationToken);

                var topCustomersList = groupedOrders.Select(g =>
                {
                    int customerId = (int)g.Key;
                    var customer = customers.FirstOrDefault(x => x.Id == customerId);
                    return new TopCustomerDTO
                    {
                        CustomerId = customerId,
                        Email = customer?.Email,
                        TotalOrders = g.Count(),
                        TotalSpent = (decimal)g.Sum(o => o.TotalPrice)
                    };
                }).OrderByDescending(x => x.TotalSpent).Take(5).ToList();

                return topCustomersList;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}