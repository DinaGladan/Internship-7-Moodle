using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoodleSystem.Domain.Entities;

namespace MoodleSystem.Infrastructure.Persistence.Configurations
{
    public sealed class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public const int TitleMaxLength = 100;
        public const int ContentMaxLength = 1000;
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.ToTable("announcements");

            builder.HasKey(a =>a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("id");

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength)
                .HasColumnName("title");

            builder.Property(a => a.Content)
                .IsRequired()
                .HasMaxLength(ContentMaxLength)
                .HasColumnName("content");

            builder.Property(a => a.CourseId)
                .IsRequired()
                .HasColumnName("course_id");

            builder.HasIndex(a => a.CourseId);

            builder.Property(a => a.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.HasOne(a => a.Course)
                .WithMany(c => c.Announcements)
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
