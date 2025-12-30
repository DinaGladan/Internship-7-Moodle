namespace MoodleSystem.Application.DTO
{
    public class Top3CoursesDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public int StudentCount { get; set; }
    }
}
