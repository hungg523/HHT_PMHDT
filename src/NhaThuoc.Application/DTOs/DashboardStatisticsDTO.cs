namespace NhaThuoc.Application.DTOs
{
    public class DashboardStatisticsDTO
    {
        public int TotalUsers { get; set; }
        public int TotalProducts { get; set; }
        public int TotalCoupons { get; set; }
        public int CouponsUsable { get; set; }
        public int CouponsNotUsable { get; set; }
        public int ActiveProducts { get; set; }
        public int InactiveProducts { get; set; }
    }
}