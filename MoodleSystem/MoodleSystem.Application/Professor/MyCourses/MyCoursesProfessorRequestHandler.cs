using MoodleSystem.Application.Common.Model;
using MoodleSystem.Application.DTO;
using MoodleSystem.Domain.Persistence.Courses;

namespace MoodleSystem.Application.Professor.MyCourses
{
    public class MyCoursesProfessorRequestHandler
    {
        private readonly ICourseRepository _courseRepository;

        public MyCoursesProfessorRequestHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<MyCoursesProfessorResponse> ProfessorHandler()
        {
            var professorId = CurrentUser.User!.Id;
            var courses = await _courseRepository.GetAllAsync();

            var professorCourses = new List<ProfessorCoursesDTO>();

            foreach(var course in courses.Values.Where(c => c.ProfessorId == professorId))
            {
                professorCourses.Add(new ProfessorCoursesDTO
                {
                    CourseId = course.Id,
                    Name = course.Name,
                    StudentCount = await _courseRepository.StudentCountAsync(course.Id)
                });
            }

            return new MyCoursesProfessorResponse
            {
                Courses = professorCourses
                .ToList()
            };
        }
    }
}