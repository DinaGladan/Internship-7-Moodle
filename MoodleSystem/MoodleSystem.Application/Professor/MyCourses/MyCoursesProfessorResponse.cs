using MoodleSystem.Application.DTO;

namespace MoodleSystem.Application.Professor.MyCourses
{
    public class MyCoursesProfessorResponse
    {
        public List<ProfessorCoursesDTO> Courses { get; init; } = new();
    }
}
