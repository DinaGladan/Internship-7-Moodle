using MoodleSystem.Application.DTO;

namespace MoodleSystem.Application.Student.StudentCoursesScreen
{
    public class StudentCorsesScreenResponse
    {
        public List<AnnouncementDTO> Announcements { get; init; } = new();
        public List<MaterialDTO> Materials { get; init; } = new();
    }
}
