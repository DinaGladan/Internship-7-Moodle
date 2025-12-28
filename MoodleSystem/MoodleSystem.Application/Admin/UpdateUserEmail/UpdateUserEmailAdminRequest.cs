namespace MoodleSystem.Application.Admin.UpdateUserEmail
{
    public class UpdateUserEmailAdminRequest
    {
        public int UserId { get; init; }
        public string UserNewEmail { get; init; } = null!;
    }
}
