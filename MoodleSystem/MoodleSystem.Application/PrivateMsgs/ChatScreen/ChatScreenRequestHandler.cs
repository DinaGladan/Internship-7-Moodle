using MoodleSystem.Application.Common.Model;
using MoodleSystem.Application.DTO;
using MoodleSystem.Domain.Persistence.PrivateMessages;

namespace MoodleSystem.Application.PrivateMsgs.ChatScreen
{
    public class ChatScreenRequestHandler
    {
        private readonly IPrivateMessageRepository _privateMessage;

        public ChatScreenRequestHandler(IPrivateMessageRepository privateMessage)
        {
            _privateMessage = privateMessage;
        }

        public async Task<ChatScreenResponse> MsgsHandler(ChatScreenRequest req)
        {
            var userId = CurrentUser.User!.Id;
            var otherUserId = req.AnotherUserId;

            var messages = await _privateMessage.GetConversationAsync(userId, otherUserId);

            return new ChatScreenResponse
            {
                Conversations = messages
                    .OrderBy(m => m.CreatedAt)
                    .Select(m => new ChatScreenDTO
                    {
                        IsMine = m.SenderId == userId,
                        Content = m.Content,
                        SentAt = m.CreatedAt
                    })
                    .ToList()
            };
        }

    }
}