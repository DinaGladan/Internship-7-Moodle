using Microsoft.EntityFrameworkCore;
using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Persistence.Courses;
using MoodleSystem.Infrastructure.Persistence;
using System.Data;

namespace MoodleSystem.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course, int>, ICourseRepository
    {
        protected readonly MoodleDbContext _moodleDb;
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
        public async Task<Course?> GetByIdWithDetailsAsync(int courseId)
        {
            return await _moodleDb.Courses
                .AsSplitQuery() //za warnning
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.User)
                .Include(c => c.Announcements)
                .Include(c => c.Materials)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task AddMaterialAsync(Material material)
        {
            await _moodleDb.Materials.AddAsync(material);
        }

        public async Task AddAnnouncementAsync(Announcement announcement)
        {
            await _moodleDb.Announcements.AddAsync(announcement);
        }
    }
}
