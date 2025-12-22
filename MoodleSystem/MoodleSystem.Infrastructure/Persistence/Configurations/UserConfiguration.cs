using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoodleSystem.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public const int NameMaxLength = 100;
        public const int EmailMaxLength = 255;
        public const int PasswordMaxLength = 100;
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(NameMaxLength)
                .HasColumnName("first_name");

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(NameMaxLength)
                .HasColumnName("last_name");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(EmailMaxLength)
                .HasColumnName("email");

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(PasswordMaxLength)
                .HasColumnName("password");

            builder.Property(u => u.Role)
                .HasConversion<int>()
                .IsRequired()
                .HasColumnName("role");

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");
        }
    }
}

