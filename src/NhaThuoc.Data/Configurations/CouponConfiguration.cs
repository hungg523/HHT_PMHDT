using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Configurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("Coupons");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Code);
            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.TimesUsed);
            builder.Property(x => x.MaxUsage);
            builder.Property(x => x.Discount);
            builder.Property(x => x.IsActive);
            builder.Property(x => x.CouponStartDate);
            builder.Property(x => x.CouponEndDate);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);
        }
    }
}
