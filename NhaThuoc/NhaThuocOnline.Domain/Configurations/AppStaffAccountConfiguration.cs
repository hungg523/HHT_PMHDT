using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuocOnline.Domain.Entities;

namespace NhaThuocOnline.Domain.Configurations
{
    public class AppStaffAccountConfiguration : IEntityTypeConfiguration<AppStaffAccount>
    {
        public void Configure(EntityTypeBuilder<AppStaffAccount> builder)
        {
            builder.ToTable("AppStaffAccounts");
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);
        }
    }
}
