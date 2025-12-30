using MoodleSystem.Application.DTO;

namespace MoodleSystem.Application.Statistics
{
    public class StatisticsResponse
    {
        public int UserCount { get; set; }
        public int CourseCount { get; set; }
        public List<Top3UsersDTO> Top3Users { get; set; } = new();
        public List<Top3CoursesDTO> Top3Courses { get; set; } = new();
    }
}