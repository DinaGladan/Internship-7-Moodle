using MoodleSystem.Application.DTO;
using MoodleSystem.Domain.DTOs;

namespace MoodleSystem.Application.Statistics
{
    public class StatisticsResponse
    {
        public List<RoleCountDTO> RoleCounts { get; set; } = new();
        public int CourseCount { get; set; }
        public List<Top3UsersDTO> Top3Users { get; set; } = new();
        public List<Top3CoursesDTO> Top3Courses { get; set; } = new();
    }
}