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

            var professorCourses = courses.Values
                .Where(c => c.ProfessorId == professorId)
                .Select(c => new ProfessorCoursesDTO
                {
                    CourseId = c.Id,
                    Name = c.Name,
                    StudentCount =  _courseRepository.StudentCountAsync(c.Id).Result
                });
            
            return new MyCoursesProfessorResponse
            {
                Courses = professorCourses
                .ToList()
            };
        }
    }
}