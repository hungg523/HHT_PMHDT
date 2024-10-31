using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Configurations
{
    public class ProductCouponConfiguration : IEntityTypeConfiguration<ProductCoupon>
    {
        public void Configure(EntityTypeBuilder<ProductCoupon> builder)
        {
            builder.ToTable("ProductCoupons");
            builder.HasNoKey();
            builder.Property(x => x.ProductId);
            builder.Property(x => x.CouponId);
        }
    }
}