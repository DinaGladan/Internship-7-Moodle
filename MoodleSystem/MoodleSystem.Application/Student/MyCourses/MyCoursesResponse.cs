using MoodleSystem.Application.DTO;

namespace MoodleSystem.Application.Student.MyCourses
{
    public class MyCoursesResponse
    {
        public List<StudentCoursesDTO> Courses { get; init; } = new();
    }
}
