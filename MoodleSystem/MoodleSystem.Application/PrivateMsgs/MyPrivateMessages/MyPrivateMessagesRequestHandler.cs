using MoodleSystem.Application.Common.Model;
using MoodleSystem.Application.DTO;
using MoodleSystem.Domain.Persistence.PrivateMessages;

namespace MoodleSystem.Application.PrivateMsgs.MyPrivateMessages
{
    public class MyPrivateMessagesRequestHandler
    {
        private readonly IPrivateMessageRepository _messageRepository;

        public MyPrivateMessagesRequestHandler(IPrivateMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<MyPrivateMessagesResponse> MessagesHandler()
        {
            var userId = CurrentUser.User!.Id;

            var messages = (await _messageRepository.GetMyMessagesAsync(userId)).ToList();

            var conversations = messages
                .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                .Select(g =>
                {
                    var lastMsg = g.OrderByDescending(m => m.CreatedAt).First();

                    var otherUser = lastMsg.SenderId == userId ? lastMsg.Receiver : lastMsg.Sender;

                    return new MyPrivateMessagesDTO
                    {
                        UserId = otherUser.Id,
                        FullName = $"{otherUser.FirstName} {otherUser.LastName}",
                        CreatedAt = lastMsg.CreatedAt,
                        Content = lastMsg.Content
                    };
                })
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return new MyPrivateMessagesResponse { PrivateMessages = conversations };
        }
    }
}