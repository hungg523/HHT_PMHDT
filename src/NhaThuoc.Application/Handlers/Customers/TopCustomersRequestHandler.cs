using MediatR;
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
            var groupedOrders = orderRepository.FindAll().GroupBy(o => o.CustomerId).ToList();
            var topCustomerTasks = groupedOrders.Select(async g =>
            {
                int customerId = (int)g.Key;
                var customer = await customerRepository.FindSingleAsync(x => x.Id == customerId);
                return new TopCustomerDTO
                {
                    CustomerId = customerId,
                    Email = customer?.Email,
                    TotalOrders = g.Count(),
                    TotalSpent = (decimal)g.Sum(o => o.TotalPrice)
                };
            }).ToList();

            var topCustomersList = await Task.WhenAll(topCustomerTasks);
            var sortedTopCustomers = topCustomersList.OrderByDescending(x => x.TotalSpent).Take(5).ToList();

            return sortedTopCustomers;
        }
    }
}