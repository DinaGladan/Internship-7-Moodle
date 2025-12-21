using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoodleSystem.Infrastructure.Persistence;

namespace MoodleSystem.Infrastructure.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MoodleDbContext>
    {
        public MoodleDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "MoodleSystem.Console"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("MoodleDbContext");
            var optionsBuilder = new DbContextOptionsBuilder<MoodleDbContext>();

            optionsBuilder.UseNpgsql(connectionString);


            return new MoodleDbContext(optionsBuilder.Options);
        }
    }
}
