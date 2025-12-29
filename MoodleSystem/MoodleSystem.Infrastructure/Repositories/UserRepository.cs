using Microsoft.EntityFrameworkCore;
using MoodleSystem.Domain.Enumerations;
using MoodleSystem.Domain.Persistence.Users;
using MoodleSystem.Infrastructure.Persistence;

namespace MoodleSystem.Infrastructure.Repositories
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        private readonly MoodleDbContext _moodleDb;
        public UserRepository(MoodleDbContext moodleDb) : base(moodleDb)
        {
            _moodleDb = moodleDb; //ili unutar repository umjesto private protected pa bi moglo bez ovoga
        }
        public async Task<bool> DoesEmailExistsAsync(string email)
        {
            return await _moodleDb.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _moodleDb.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetByRoleAsync(UserRole role)
        {
            return await _moodleDb.Users
                .Where(u => u.Role == role)
                .OrderBy(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersWithoutChatAsync(int currentUserId)
        {
            var chattedUserIds = await _moodleDb.PrivateMessages
                .Where(pm => pm.SenderId == currentUserId || pm.ReceiverId == currentUserId)
                .Select(pm => pm.SenderId == currentUserId ? pm.ReceiverId : pm.SenderId)
                .Distinct()
                .ToListAsync();

            return await _moodleDb.Users
                .Where(u => u.Id != currentUserId && !chattedUserIds.Contains(u.Id))
                .OrderBy(u => u.FirstName)
                .ToListAsync();
        }

    }
}


