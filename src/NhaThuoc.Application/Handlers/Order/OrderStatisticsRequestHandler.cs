using MediatR;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Application.Request.Order;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.Enums;

namespace NhaThuoc.Application.Handlers.Order
{
    public class OrderStatisticsRequestHandler : IRequestHandler<OrderStatisticsRequest, OrderStatisticsDTO>
    {
        private readonly IOrderRepository orderRepository;

        public OrderStatisticsRequestHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<OrderStatisticsDTO> Handle(OrderStatisticsRequest request, CancellationToken cancellationToken)
        {
            var totalOrders = orderRepository.FindAll().Count();
            var totalRevenue = orderRepository.FindAll().Sum(o => o.TotalPrice);
            var totalPendingOrders = orderRepository.FindAll(o => o.Status == OrderStatus.Pending).Count();
            var totalCompletedOrders = orderRepository.FindAll(o => o.Status == OrderStatus.Successed).Count();

            var monthlyRevenues = orderRepository.FindAll()
                .Where(o => o.CreatedAt.HasValue)
                .GroupBy(o => new { o.CreatedAt.Value.Month, o.CreatedAt.Value.Year })
                .Select(g => new MonthlyRevenueDTO
                {
                    Month = g.Key.Month,
                    TotalRevenue = g.Sum(o => o.TotalPrice.Value)
                }).ToList();

            var yearlyRevenues = orderRepository.FindAll()
                .Where(o => o.CreatedAt.HasValue)
                .GroupBy(o => o.CreatedAt.Value.Year)
                .Select(g => new YearlyRevenueDTO
                {
                    Year = g.Key,
                    TotalRevenue = g.Sum(o => o.TotalPrice.Value)
                })
                .ToList();

            var order = new OrderStatisticsDTO
            {
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                TotalPendingOrders = totalPendingOrders,
                TotalCompletedOrders = totalCompletedOrders,
                MonthlyRevenues = monthlyRevenues,
                YearlyRevenues = yearlyRevenues
            };

            return order;
        }
    }
}