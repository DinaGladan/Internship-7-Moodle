namespace MoodleSystem.Application.DTO
{
    public class MyPrivateMessagesDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

    }
}
