using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").UseIdentityColumn();
            builder.Property(x => x.ParentId).HasColumnName("ParentId");
            builder.Property(x => x.Name).HasColumnName("CategoryName");
            builder.Property(x => x.Description).HasColumnName("CategoryDescription");
            builder.Property(x => x.ImagePath).HasColumnName("ImagePath");
            builder.Property(x => x.IsActive).HasColumnName("IsActive");
            builder.Property(x => x.CreatedAt).HasColumnName("CreatedAt");
            builder.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt");
        }
    }
}
