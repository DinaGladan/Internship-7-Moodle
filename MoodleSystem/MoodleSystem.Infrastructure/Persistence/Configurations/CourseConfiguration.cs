using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoodleSystem.Domain.Entities;

namespace MoodleSystem.Infrastructure.Persistence.Configurations
{
    public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public int NameMaxLength = 100;
        public void Configure(EntityTypeBuilder<Course> builder) {
            builder.ToTable("courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength)
                .HasColumnName("name");

            builder.Property(c => c.ProfessorId)
                .IsRequired()
                .HasColumnName("professor_id");

            builder.HasIndex(c => c.ProfessorId);

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.HasOne(c => c.Professor)
                .WithMany()
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}