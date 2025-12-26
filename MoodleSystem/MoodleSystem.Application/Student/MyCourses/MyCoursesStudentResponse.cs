using MoodleSystem.Application.DTO;

namespace MoodleSystem.Application.Student.MyCourses
{
    public class MyCoursesStudentResponse
    {
        public List<StudentCoursesDTO> Courses { get; init; } = new();
    }
}
