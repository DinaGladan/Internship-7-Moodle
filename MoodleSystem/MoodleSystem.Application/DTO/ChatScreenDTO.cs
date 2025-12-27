namespace MoodleSystem.Application.DTO
{
    public class ChatScreenDTO
    {
        public bool IsMine { get; init; }
        public DateTime SentAt { get; init; }
        public string Content { get; init; } = null!;

    }
}
