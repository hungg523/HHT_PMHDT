﻿using Microsoft.EntityFrameworkCore;
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
            builder.Property(x => x.Id).HasColumnName("ProductId");
            builder.Property(x => x.ProductName).HasColumnName("ProductName");
            builder.HasIndex(x => x.ProductName);
            builder.Property(x => x.RegularPrice).HasColumnName("RegularPrice");
            builder.Property(x => x.DiscountPrice).HasColumnName("DiscountPrice");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.Property(x => x.Brand).HasColumnName("Brand");
            builder.Property(x => x.Packaging).HasColumnName("Packaging");
            builder.Property(x => x.Origin).HasColumnName("Origin");
            builder.Property(x => x.Manufacturer).HasColumnName("Manufacturer");
            builder.Property(x => x.Ingredients).HasColumnName("Ingredients");
            builder.Property(x => x.ImagePath).HasColumnName("ImagePath");
            builder.Property(x => x.SeoTitle).HasColumnName("SeoTitle");
            builder.Property(x => x.SeoAlias).HasColumnName("SeoAlias");
            builder.Property(x => x.CreatedAt).HasColumnName("CreatedAt");
            builder.Property(x => x.IsActived).HasColumnName("IsActive");

            builder.HasMany(x => x.ProductCategories).WithOne().HasForeignKey(x => x.ProductId);
        }
    }
}
