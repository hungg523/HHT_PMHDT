using MediatR;
using Microsoft.EntityFrameworkCore;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class DashboardStatisticsRequestHandler : IRequestHandler<DashboardStatisticsRequest, DashboardStatisticsDTO>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IProductRepository productRepository;
        private readonly ICouponRepository couponRepository;

        public DashboardStatisticsRequestHandler(ICustomerRepository customerRepository, IProductRepository productRepository, ICouponRepository couponRepository)
        {
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
            this.couponRepository = couponRepository;
        }

        public async Task<DashboardStatisticsDTO> Handle(DashboardStatisticsRequest request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var totalUsersTask = customerRepository.FindAll().Count();
            var totalProductsTask = productRepository.FindAll().Count();
            var totalCouponsTask = couponRepository.FindAll().Count();
            var couponsUsableTask = couponRepository.FindAll().Count(c => c.IsActive && c.CouponStartDate <= now && c.CouponEndDate >= now && c.TimesUsed < c.MaxUsage);
            var couponsNotUsableTask = couponRepository.FindAll().Count(c => !c.IsActive || c.CouponStartDate > now || c.CouponEndDate < now || c.TimesUsed >= c.MaxUsage);
            var activeProductsTask = productRepository.FindAll().Count(p => (bool)p.IsActived);
            var inactiveProductsTask = productRepository.FindAll().Count(p => (bool)!p.IsActived);

            var dashboard = new DashboardStatisticsDTO
            {
                TotalUsers = totalUsersTask,
                TotalProducts = totalProductsTask,
                TotalCoupons = totalCouponsTask,
                CouponsUsable = couponsUsableTask,
                CouponsNotUsable = couponsNotUsableTask,
                ActiveProducts = activeProductsTask,
                InactiveProducts = inactiveProductsTask
            };

            return dashboard;
        }
    }
}