using MoodleSystem.Application.DTO;
using MoodleSystem.Application.PrivateMsgs.ChatScreen;
using MoodleSystem.Application.PrivateMsgs.MyPrivateMessages;
using MoodleSystem.Application.PrivateMsgs.NewMessage;

namespace MoodleSystem.Console.Actions
{
    public class PrivateMessageAction
    {
        private readonly MyPrivateMessagesRequestHandler _myPrivateMessagesRequestHandler;
        private readonly ChatScreenRequestHandler _chatScreenRequestHandler;
        private readonly NewMessageRequestHandler _newMessageRequestHandler;
        private readonly NewMessageUsersRequestHandler _newUsersRequestHandler;

        public PrivateMessageAction(MyPrivateMessagesRequestHandler myPrivateMessagesRequestHandler, ChatScreenRequestHandler chatScreenRequestHandler, NewMessageRequestHandler newMessageRequestHandler, NewMessageUsersRequestHandler newUsersRequestHandler)
        {
            _myPrivateMessagesRequestHandler = myPrivateMessagesRequestHandler;
            _chatScreenRequestHandler = chatScreenRequestHandler;
            _newMessageRequestHandler = newMessageRequestHandler;
            _newUsersRequestHandler = newUsersRequestHandler;
        }

        public async Task<MyPrivateMessagesResponse> GetMyConversations()
        {
            return await _myPrivateMessagesRequestHandler.MessagesHandler();
        }
        public async Task<List<UsersListDTO>> GetUsersForNewMessage()
        {
            return await _newUsersRequestHandler.MsgHandler();
        }

        public async Task<ChatScreenResponse> OpenChat(int anotherUserId)
        {
            return await _chatScreenRequestHandler.MsgsHandler(new ChatScreenRequest { AnotherUserId = anotherUserId });
        }

        public async Task SendMessage(int reciverId, string content)
        {
            await _newMessageRequestHandler.MessageHandler(new NewMessageRequest
            {
                ReceiverId = reciverId,
                Content = content
            });
        }
    }
}