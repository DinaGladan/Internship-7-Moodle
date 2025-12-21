using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Persistence.Common;

namespace MoodleSystem.Domain.Persistence.PrivateMessages
{
    public interface IPrivateMessageRepository : IRepository<PrivateMessage, int >
    {
        Task<IEnumerable<PrivateMessage>>GetConversationAsync(int senderId, int receiverId);
        Task<int> GetSentMessagesCount(int senderId, DateTime startDate, DateTime endDate);

    }
}
