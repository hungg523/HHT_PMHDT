using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(200);
            builder.Property(x => x.LastName).HasMaxLength(200);
            builder.Property(x => x.AvatarImagePath).HasMaxLength(200);
            builder.Property(x => x.PhoneNumber);
            builder.Property(x => x.Email);
            builder.Property(x => x.Password);
            builder.Property(x => x.Role);
            builder.Property(x => x.IsActive);
            builder.Property(x => x.CreatedAt);
        }
    }
}
