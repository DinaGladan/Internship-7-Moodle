using MoodleSystem.Domain.Enumerations;

namespace MoodleSystem.Application.Admin.ChangeUserRole
{
    public class ChangeUserRoleRequest
    {
        public int UserId { get; init; }
        public UserRole NewRole { get; init; }
    }
}
