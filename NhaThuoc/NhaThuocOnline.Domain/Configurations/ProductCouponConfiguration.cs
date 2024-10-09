using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuocOnline.Domain.Entities;

namespace NhaThuocOnline.Domain.Configurations
{
    public class ProductCouponConfiguration : IEntityTypeConfiguration<ProductCoupon>
    {
        public void Configure(EntityTypeBuilder<ProductCoupon> builder)
        {
            builder.ToTable("ProductCoupons");
            builder.HasNoKey();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.CouponId).IsRequired();


        }
    }
}
