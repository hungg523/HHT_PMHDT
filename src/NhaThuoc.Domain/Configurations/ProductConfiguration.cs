using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Domain.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ProductName).HasMaxLength(200).IsRequired();
            builder.HasIndex(x => x.ProductName);
            builder.Property(x => x.SKU).IsRequired();
            builder.Property(x => x.RegularPrice).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired();
            builder.Property(x => x.SeoTitle).HasMaxLength(200);
            builder.Property(x => x.SeoAlias).HasMaxLength(200);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();
            builder.Property(x => x.DiscountPrice).IsRequired();
            builder.Property(x => x.Brand).IsRequired();
            builder.Property(x => x.Packaging).IsRequired();
            builder.Property(x => x.Origin).IsRequired();
            builder.Property(x => x.Manufacturer).IsRequired();
            builder.Property(x => x.Ingredients).IsRequired();
            builder.Property(x => x.IsActived).IsRequired();
        }
    }
}