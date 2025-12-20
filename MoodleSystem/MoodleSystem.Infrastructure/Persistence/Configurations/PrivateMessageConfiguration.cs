using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoodleSystem.Domain.Entities;

namespace MoodleSystem.Infrastructure.Persistence.Configurations
{
    public sealed class PrivateMessageConfiguration : IEntityTypeConfiguration<PrivateMessage>
    {
        public int ContentMaxLength = 1000;
        public void Configure(EntityTypeBuilder<PrivateMessage> builder)
        {
            builder.ToTable("private_messages");

            builder.HasKey(pm => pm.Id);

            builder.Property(pm => pm.Id)
                .HasColumnName("id");

            builder.Property(pm => pm.Content)
                .IsRequired()
                .HasMaxLength(ContentMaxLength)
                .HasColumnName("content");

            builder.Property(pm => pm.SenderId)
                .IsRequired()
                .HasColumnName("sender_id");

            builder.HasIndex(pm => pm.SenderId);

            builder.Property(pm => pm.ReceiverId)
                .IsRequired()
                .HasColumnName("receiver_id");

            builder.HasIndex(pm => pm.ReceiverId);

            builder.Property(pm => pm.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.HasOne(pm => pm.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(pm => pm.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pm => pm.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(pm => pm.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
