using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MoodleSystem.Application.DTO
{
    public class UsersListDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
