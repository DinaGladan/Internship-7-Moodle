using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoodleSystem.Domain.Persistence.Common;
using MoodleSystem.Domain.Persistence.Courses;
using MoodleSystem.Domain.Persistence.PrivateMessages;
using MoodleSystem.Domain.Persistence.Statistics;
using MoodleSystem.Domain.Persistence.Users;
using MoodleSystem.Infrastructure.Persistence;
using MoodleSystem.Infrastructure.Repositories;

namespace MoodleSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddDatabase(services, configuration);
            AddRepositories(services);
            return services;
        }

        private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MoodleDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("MoodleDbContext")));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IPrivateMessageRepository, PrivateMessageRepository>();
            services.AddScoped<IStatisticRepository, StatisticRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}