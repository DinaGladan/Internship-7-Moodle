using MoodleSystem.Application.Common.Model;
using MoodleSystem.Application.DTO;
using MoodleSystem.Domain.Persistence.Courses;

namespace MoodleSystem.Application.Professor.ProfessorCoursesScreen
{
    public class ProfessorCoursesScreenRequestHandler
    {
        private readonly ICourseRepository _courseRepository;

        public ProfessorCoursesScreenRequestHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<ProfessorCoursesScreenResponse> CoursesHandler(ProfessorCoursesScreenRequest req)
        {
            var professorId = CurrentUser.User!.Id;

            var course = await _courseRepository.GetByIdAsync(req.CourseId);

            if(course == null || course.ProfessorId != professorId)
                return new ProfessorCoursesScreenResponse();

            var students = course.Enrollments
                .Select(e => e.User)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new StudentDTO
                {
                    StudentId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                })
                .ToList();

            var announcements = course.Announcements
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new AnnouncementDTO
                {
                    Title = a.Title,
                    Content = a.Content,
                    CreatedAt = a.CreatedAt
                })
                .ToList();

            var materials = course.Materials
                .OrderByDescending(m => m.CreatedAt)
                .Select(m => new MaterialDTO
                {
                    Name = m.Name,
                    Url = m.Url,
                    CreatedAt = m.CreatedAt
                })
                .ToList();

            return new ProfessorCoursesScreenResponse
            {
                Students = students,
                Announcements = announcements,
                Materials = materials
            };

        }
    }
}