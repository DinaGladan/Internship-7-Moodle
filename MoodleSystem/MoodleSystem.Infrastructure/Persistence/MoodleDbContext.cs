using Microsoft.EntityFrameworkCore;
using MoodleSystem.Domain.Entities;

namespace MoodleSystem.Infrastructure.Persistence
{
    public class MoodleDbContext : DbContext
    {
        public MoodleDbContext(DbContextOptions<MoodleDbContext> options) : base(options) { }  // predstavlja spajanje s bazom

        public DbSet<User> Users { get; set; } = null!; // predstavlja tablicu u bazi
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Announcement> Announcements { get; set; } = null!;
        public DbSet<Material> Materials { get; set; } = null!;
        public DbSet<PrivateMessage> PrivateMessages { get; set; } = null!;
        public DbSet<UserCourse> UserCourses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoodleDbContext).Assembly);

            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
