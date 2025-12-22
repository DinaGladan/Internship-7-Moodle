namespace MoodleSystem.Domain.Abstractions
{
    public class BaseEntity
    {
        public int Id { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
