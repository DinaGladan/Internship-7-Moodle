using Microsoft.EntityFrameworkCore;
using MoodleSystem.Domain.DTOs;
using MoodleSystem.Domain.Persistence.Statistics;
using MoodleSystem.Infrastructure.DTO;
using MoodleSystem.Infrastructure.Persistence;

namespace MoodleSystem.Infrastructure.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly MoodleDbContext _moodleDb;

        public StatisticRepository(MoodleDbContext moodleDb)
        {
            _moodleDb = moodleDb;
        }
        public async Task<int> CountCoursesAsync(DateTime startDate, DateTime endDate)
        {
            return await _moodleDb.Courses
                .CountAsync(c => c.CreatedAt >= startDate && c.CreatedAt <= endDate);
        }

        public async Task<int> CountUsersAsync(DateTime startDate, DateTime endDate)
        {
            return await _moodleDb.Users
                .CountAsync(u => u.CreatedAt >= startDate && u.CreatedAt <= endDate);
        }

        public async Task<IEnumerable<CourseStudentCountDTO>> Top3CoursesByStudentsAsync(DateTime startDate, DateTime endDate)
        {
            return await _moodleDb.UserCourses
                .Where(uc => uc.Course.CreatedAt >= startDate && uc.Course.CreatedAt <= endDate)
                .GroupBy(uc => uc.CourseId) //IGrouping<int, UserCourse>
                .Select(g => new CourseStudentCountDTO { CourseId = g.Key, Count = g.Count() })
                .OrderByDescending(csc => csc.Count)
                .Take(3)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserMessageCountDTO>> Top3UsersBySentMessagesAsync(DateTime startDate, DateTime endDate)
        {
            return await _moodleDb.PrivateMessages
               .Where(pm => pm.CreatedAt >= startDate && pm.CreatedAt <= endDate)
               .GroupBy(pm => pm.SenderId) //IGrouping<int, UserCourse>
               .Select(g => new UserMessageCountDTO { SenderId = g.Key, Count = g.Count() })
               .OrderByDescending(umc => umc.Count)
               .Take(3)
               .ToListAsync();
        }

        public async Task<IEnumerable<RoleCountDTO>> GetUsersCountByRole(DateTime startTime, DateTime endTime)
        {
            return await _moodleDb.Users
                .Where(u => u.CreatedAt >= startTime && u.CreatedAt <= endTime)
                .GroupBy(u => u.Role)
                .Select(u => new RoleCountDTO
                {
                    Role = u.Key.ToString(),
                    Count = u.Count()
                })
                .OrderByDescending(u => u.Count)
                .ToListAsync();
        }
    }
}
