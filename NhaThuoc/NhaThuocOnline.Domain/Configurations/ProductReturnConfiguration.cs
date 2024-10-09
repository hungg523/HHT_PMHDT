using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuocOnline.Domain.Entities;

namespace NhaThuocOnline.Domain.Configurations
{
    public class ProductReturnConfiguration : IEntityTypeConfiguration<ProductReturn>
    {
        public void Configure(EntityTypeBuilder<ProductReturn> builder)
        {
            builder.ToTable("ProductReturns");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();

        }
    }
}
