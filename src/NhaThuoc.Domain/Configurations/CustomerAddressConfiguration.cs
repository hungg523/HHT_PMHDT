using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Domain.Configurations
{
    public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.ToTable("CustomerAddresses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(200).IsRequired();
            builder.Property(x => x.FullName).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.Province).IsRequired();
            builder.Property(x => x.District).IsRequired();
            builder.Property(x => x.Ward).IsRequired();
        }
    }
}