namespace MoodleSystem.Application.DTO
{
    public class AnnouncementDTO
    {
        public string Title { get; init; } = null!;
        public string Content { get; init; } = null!;
        public string Professor { get; init; } = null!;
        public DateTime CreatedAt { get; init; }

    }
}
