namespace MoodleSystem.Application.NewMessage
{
    public class NewMessageRequest
    {
        public int ReceiverId { get; init; }
        public string Content { get; init; } = null!;
    }
}
