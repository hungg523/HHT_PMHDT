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
            builder.Property(x => x.Id).HasJsonPropertyName("ApplyCouponId");
            builder.Property(x => x.OrderId).HasJsonPropertyName("OrderId");
            builder.Property(x => x.CouponId).HasJsonPropertyName("CouponId");
            builder.Property(x => x.DiscoundAmount).HasJsonPropertyName("DiscoundAmount").HasPrecision(18, 2);
            builder.HasKey(x => new
            {
                x.Id,
                x.OrderId,
                x.CouponId
            });
        }
    }
}