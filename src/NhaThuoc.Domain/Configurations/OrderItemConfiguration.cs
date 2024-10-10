using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Domain.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {

        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();


            builder.Property(x => x.CreatedAt).IsRequired(); 
            builder.Property(x => x.UpdatedAt).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired();


        }

    }
}
