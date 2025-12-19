using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Persistence.Common;

namespace MoodleSystem.Domain.Persistence.PrivateMessages
{
    public interface IPrivateMessageRepository : IRepository<PrivateMessage, int >
    {
        Task<IEnumerable<PrivateMessage>>GetConversationAsync(int firstUserId, int secondUserId);
        Task<int> GetSentMessagesCount(int userId, DateTime startDate, DateTime endDate);

    }
}
