using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Configurations
{
    public class AdminMessageConfiguration : IEntityTypeConfiguration<AdminMessage>
    {
        public void Configure(EntityTypeBuilder<AdminMessage> builder)
        {
            builder.ToTable("AdminMessage");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Message).HasColumnName("Message");
            builder.Property(x => x.ConversationId).HasColumnName("ConversationId");
            builder.Property(x => x.CreateDate).HasColumnName("RespondedAt");
        }
    }
}