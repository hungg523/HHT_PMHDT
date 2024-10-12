using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Domain.Configurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("Coupons");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
            builder.Property(x => x.TimesUsed).IsRequired();
            builder.Property(x => x.MaxUsage).IsRequired();
            builder.Property(x => x.CouponStartDate).IsRequired();
            builder.Property(x => x.CouponEndDate).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();
            builder.Property(x => x.Discount).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}