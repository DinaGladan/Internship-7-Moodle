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

            var messages = await _messageRepository.GetConversationAsync(userId, userId);

            var myPrivateMesseges = messages.GroupBy(m => m.SenderId == userId ? m.Receiver : m.Sender)
                .Select(g =>
                {
                    var lastmsg = g.OrderByDescending(m => m.CreatedAt).First();
                    return new MyPrivateMessagesDTO
                    {
                        UserId = g.Key.Id,
                        FullName = $"{g.Key.FirstName} {g.Key.LastName}",
                        CreatedAt = lastmsg.CreatedAt,
                        Content = lastmsg.Content
                    };
                })
                .OrderByDescending(m => m.CreatedAt)
                .ToList();

            return new MyPrivateMessagesResponse { PrivateMessages = myPrivateMesseges };
        }
    }
}