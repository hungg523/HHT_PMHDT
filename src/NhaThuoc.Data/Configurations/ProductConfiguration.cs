using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ProductName).HasMaxLength(200);
            builder.HasIndex(x => x.ProductName);
            builder.Property(x => x.SKU);
            builder.Property(x => x.RegularPrice);
            builder.Property(x => x.DiscountPrice);
            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.Brand);
            builder.Property(x => x.Packaging);
            builder.Property(x => x.Origin);
            builder.Property(x => x.Manufacturer);
            builder.Property(x => x.Ingredients);
            builder.Property(x => x.ImagePath).HasMaxLength(200);
            builder.Property(x => x.SeoTitle).HasMaxLength(200);
            builder.Property(x => x.SeoAlias).HasMaxLength(200);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.IsActived);
        }
    }
}
