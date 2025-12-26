namespace MoodleSystem.Application.DTO
{
    public class ProfessorCoursesDTO
    {
        public string Name { get; init; } = null!;
        public int CourseId { get; init; }
        public int StudentCount { get; init; }
    }
}
