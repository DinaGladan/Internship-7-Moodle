using MoodleSystem.Application.Common.Model;
using MoodleSystem.Application.DTO;
using MoodleSystem.Domain.Persistence.Courses;

namespace MoodleSystem.Application.Student.MyCourses
{
    public class MyCoursesRequestHandler
    {
        private readonly ICourseRepository _courseRepository;
        public MyCoursesRequestHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<MyCoursesResponse> StudentHandler()
        {
            var studentId = CurrentUser.User!.Id;
            var courses = await _courseRepository.GetAllAsync();

            var studentCourses = courses.Values
                .Where(c => c.Enrollments.Any(e => e.UserId == studentId))
                .OrderBy(c => c.Name)
                .Select(c => new StudentCoursesDTO
                {
                    CourseId = c.Id,
                    Name = c.Name,
                    Professor = $"{c.Professor.FirstName} {c.Professor.LastName}" ,
                })
                .ToList();

            return new MyCoursesResponse
            {
                Courses = studentCourses
            };
        }

    }
}
