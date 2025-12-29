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

        public async Task<IEnumerable<PrivateMessage>> GetConversationAsync(int me, int other)
        {
            return await _moodleDb.PrivateMessages
                .Where(pm =>
                    (pm.SenderId == me && pm.ReceiverId == other) ||
                    (pm.SenderId == other && pm.ReceiverId == me))
                .Include(pm => pm.Sender)
                .Include(pm => pm.Receiver)
                .OrderBy(pm => pm.CreatedAt)
                .ToListAsync();
        }
        public async Task<int> GetSentMessagesCount(int senderId, DateTime startDate, DateTime endDate)
        {
            return await _moodleDb.PrivateMessages
                .Where(pm => pm.SenderId == senderId && pm.CreatedAt >= startDate && pm.CreatedAt <= endDate)
                .CountAsync();
        }

        public async Task<IEnumerable<PrivateMessage>> GetMyMessagesAsync(int userId)
        {
            return await _moodleDb.PrivateMessages
                .Where(pm => pm.SenderId == userId || pm.ReceiverId == userId)
                .Include(pm => pm.Sender)
                .Include(pm => pm.Receiver)
                .OrderBy(pm => pm.CreatedAt)
                .ToListAsync();
        }



    }
}
