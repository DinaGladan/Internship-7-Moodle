namespace MoodleSystem.Domain.Abstractions
{
    public class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    }
}
