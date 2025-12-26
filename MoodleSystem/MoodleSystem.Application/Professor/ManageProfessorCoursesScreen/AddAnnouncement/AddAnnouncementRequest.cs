namespace MoodleSystem.Application.Professor.ManageProfessorCoursesScreen.AddAnnouncement
{
    public class AddAnnouncementRequest
    {
        public int CourseId { get; init; }
        public string Title { get; init; } = null!;
        public string Content { get; init; } = null!;
    }
}
