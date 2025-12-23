namespace MoodleSystem.Application.Common.Model.LogIn
{
    public class LogInResponse
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
        public User? User { get; init; }
    }
}
