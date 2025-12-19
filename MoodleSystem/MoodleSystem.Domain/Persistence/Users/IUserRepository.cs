using MoodleSystem.Domain.Enumerations;
using MoodleSystem.Domain.Persistence.Common;

namespace MoodleSystem.Domain.Persistence.Users
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> DoesEmailExistsAsync(string email);
        Task<IEnumerable<User>> GetByRoleAsync(UserRole role);
    }
}

