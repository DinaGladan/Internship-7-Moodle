using Microsoft.EntityFrameworkCore;
using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Persistence.Courses;
using MoodleSystem.Infrastructure.Persistence;
using System.Data;

namespace MoodleSystem.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course, int>, ICourseRepository
    {
        private readonly MoodleDbContext _moodleDb;
        public CourseRepository(MoodleDbContext moodleDb) : base(moodleDb)
        {
            _moodleDb = moodleDb;
        }

        public async Task<Course?> GetByNameAsync(string name)
        {
            return await _moodleDb.Courses.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Course>> GetByProfessorIdAsync(int professorId)
        {
            return await _moodleDb.Courses
            .Where(c => c.ProfessorId == professorId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
        }

        public async Task<int> StudentCountAsync(int courseId)
        {
            return await _moodleDb.UserCourses.CountAsync(uc => uc.CourseId == courseId);
        }
        public async Task<IEnumerable<Course>> GetAllWithEnrollmentsAsync()
        {
            return await _moodleDb.Courses
                .Include(c => c.Enrollments)
                .Include(c => c.Professor)
                .ToListAsync();
        }

    }
}
