using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Configurations
{
    public class UserMessageConfiguration : IEntityTypeConfiguration<UserMessage>
    {
        public void Configure(EntityTypeBuilder<UserMessage> builder)
        {
            builder.ToTable("UserMessage");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Message).HasColumnName("Message");
            builder.Property(x => x.ConversationId).HasColumnName("ConversationId");
            builder.Property(x => x.CreateDate).HasColumnName("SentAt");
        }
    }
}