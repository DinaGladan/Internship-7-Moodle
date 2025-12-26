using MoodleSystem.Application.DTO;

namespace MoodleSystem.Application.Professor.ProfessorCoursesScreen
{
    public class ProfessorCoursesScreenResponse
    {
        public List<AnnouncementDTO> Announcements { get; init; } = new();
        public List<MaterialDTO> Materials { get; init; } = new();
        public List<StudentDTO> Students { get; init; } = new();
    }
}
