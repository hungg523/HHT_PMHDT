using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuocOnline.Domain.Entities;

namespace NhaThuocOnline.Domain.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");


            builder.Property(x => x.Id).UseIdentityColumn();
          
            builder.Property(x => x.CouponId).IsRequired();
            builder.Property(x => x.CustomerId).IsRequired();
            

        }
    }
}
