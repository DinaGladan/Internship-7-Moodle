namespace MoodleSystem.Application.DTO
{
    public class Top3UsersDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public int MessageCount { get; set; }
    }
}
