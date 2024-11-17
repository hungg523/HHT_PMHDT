using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Configurations
{
    public class ApplyCouponConfiguration : IEntityTypeConfiguration<ApplyCoupon>
    {
        public void Configure(EntityTypeBuilder<ApplyCoupon> builder)
        {
            builder.ToTable("ApplyCoupons");
            builder.Property(x => x.OrderId).HasJsonPropertyName("OrderId");
            builder.Property(x => x.CouponId).HasJsonPropertyName("CouponId");
            builder.HasKey(x => new
            {
                x.OrderId,
                x.CouponId
            });
        }
    }
}