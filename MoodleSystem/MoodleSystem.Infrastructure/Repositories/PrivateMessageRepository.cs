using Microsoft.EntityFrameworkCore;
using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Persistence.PrivateMessages;
using MoodleSystem.Infrastructure.Persistence;

namespace MoodleSystem.Infrastructure.Repositories
{
    public class PrivateMessageRepository : Repository<PrivateMessage, int>, IPrivateMessageRepository
    {
        private readonly MoodleDbContext _moodleDb;
        public PrivateMessageRepository(MoodleDbContext moodleDb) : base(moodleDb)
        {
            _moodleDb = moodleDb;
        }

        public async Task<IEnumerable<PrivateMessage>> GetConversationAsync(int senderId, int receiverId)
        {
            return await _moodleDb.PrivateMessages
                .Where(pm => (pm.SenderId == senderId && pm.ReceiverId == receiverId) ||
                (pm.SenderId == receiverId && pm.ReceiverId == senderId))
                .OrderBy(pm => pm.CreatedAt)
                .ToListAsync();
        }
        public async Task<int> GetSentMessagesCount(int senderId, DateTime startDate, DateTime endDate)
        {
            return await _moodleDb.PrivateMessages
                .Where(pm => pm.SenderId == senderId && pm.CreatedAt >= startDate && pm.CreatedAt <= endDate)
                .CountAsync();
        }
    }
}
