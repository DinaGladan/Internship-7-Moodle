using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoodleSystem.Domain.Entities;

namespace MoodleSystem.Infrastructure.Persistence.Configurations
{
    public sealed class UserCourseConfiguration : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.ToTable("user_courses");

            builder.HasKey(uc => new { uc.UserId, uc.CourseId});

            builder.Property(uc => uc.UserId)
                .HasColumnName("user_id");

            builder.Property(uc => uc.CourseId)
                .HasColumnName("course_id");

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uc => uc.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(uc => uc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
