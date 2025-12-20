using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoodleSystem.Domain.Entities;

namespace MoodleSystem.Infrastructure.Persistence.Configurations
{
    public sealed class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public int NameMaxLength = 100;
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("materials");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasColumnName("id");

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength)
                .HasColumnName("name");

            builder.Property(m => m.Url)
                .IsRequired()
                .HasColumnName("url");

            builder.Property(m => m.CourseId)
                .IsRequired()
                .HasColumnName("course_id");

            builder.HasIndex(m => m.CourseId);

            builder.Property(m => m.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.HasOne(m => m.Course)
                .WithMany(c => c.Materials)
                .HasForeignKey(m => m.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
