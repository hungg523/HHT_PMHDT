using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.ToTable("Conversation");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ConversationId");
            builder.Property(x => x.CustomerId).HasColumnName("CustomerId");
            builder.Property(x => x.CreateDate).HasColumnName("StartedAt");
            builder.Property(x => x.LastMessageAt).HasColumnName("LastMessageAt");

            //builder.HasMany(x => x.UserMessages).WithOne().HasForeignKey(x => x.ConversationId);
            //builder.HasMany(x => x.AdminMessages).WithOne().HasForeignKey(x => x.ConversationId);
        }
    }
}