using MediatR;

namespace NhaThuoc.Application.DTOs
{
    public class MonthlyRevenueDTO
    {
        public int Month { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class YearlyRevenueDTO
    {
        public int Year { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class OrderStatisticsDTO
    {
        public int TotalOrders { get; set; }
        public decimal? TotalRevenue { get; set; }
        public int TotalPendingOrders { get; set; }
        public int TotalCompletedOrders { get; set; }
        public List<MonthlyRevenueDTO> MonthlyRevenues { get; set; }
        public List<YearlyRevenueDTO> YearlyRevenues { get; set; }
    }
}