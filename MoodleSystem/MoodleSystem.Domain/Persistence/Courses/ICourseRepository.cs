using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Persistence.Common;

namespace MoodleSystem.Domain.Persistence.Courses
{
    public interface ICourseRepository : IRepository<Course, int>
    {
        Task<Course?>GetByNameAsync(string name);
        Task<IEnumerable<Course>>GetByProfessorIdAsync(int professorId);
        Task<int>StudentCountAsync(int courseId);
        Task<IEnumerable<Course>> GetAllWithEnrollmentsAsync();
        Task<Course?> GetByIdWithDetailsAsync(int courseId);
    }
}
