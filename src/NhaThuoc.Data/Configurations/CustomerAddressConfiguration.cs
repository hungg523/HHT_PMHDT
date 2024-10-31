using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Configurations
{
    public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.ToTable("CustomerAddresses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.CustomerId);
            builder.Property(x => x.Address).HasMaxLength(200);
            builder.Property(x => x.FullName);
            builder.Property(x => x.Phone);
            builder.Property(x => x.Province);
            builder.Property(x => x.District);
            builder.Property(x => x.Ward);
        }
    }
}
