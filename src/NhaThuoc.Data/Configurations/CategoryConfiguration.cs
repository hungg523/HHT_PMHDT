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
            builder.Property(x => x.Name).HasColumnName("CategoryName").IsRequired();
            builder.Property(x => x.ImagePath).HasColumnName("ImagePath").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsRequired();
            builder.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").IsRequired();
            builder.Property(x => x.ParentId).HasColumnName("ParentId").IsRequired();
            builder.Property(x => x.Description).HasColumnName("CategoryDescription").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName("IsActive").IsRequired();
        }
    }
}
