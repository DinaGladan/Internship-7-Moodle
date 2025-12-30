using MoodleSystem.Application.DTO;
using MoodleSystem.Domain.Persistence.Courses;

namespace MoodleSystem.Application.Student.StudentCoursesScreen
{
    public class StudentCorsesScreenRequestHandler
    {
        private readonly ICourseRepository _courseRepository;

        public StudentCorsesScreenRequestHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<StudentCorsesScreenResponse> StudentHandler(StudentCoursesScreenRequest req)
        {

            var course = await _courseRepository.GetByIdWithDetailsAsync(req.CourseId);

            var announcements = course!.Announcements
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new AnnouncementDTO
                {
                    Title = a.Title,
                    Content = a.Content,
                    Professor = $"{course.Professor.FirstName} {course.Professor.LastName}",
                    CreatedAt = a.CreatedAt
                })
                .ToList();

            var materials = course!.Materials
                .OrderByDescending(m => m.CreatedAt)
                .Select(m => new MaterialDTO
                {
                    Name = m.Name,
                    Url = m.Url,
                    CreatedAt = m.CreatedAt
                })
                .ToList();

            return new StudentCorsesScreenResponse
            {
                Announcements = announcements,
                Materials = materials
            };
        }

    }
}
