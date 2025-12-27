using MoodleSystem.Application.Common.Model;
using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Persistence.Common;

namespace MoodleSystem.Application.PrivateMsgs.NewMessage
{
    public class NewMessageRequestHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewMessageRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NewMessageResponse> MessageHandler(NewMessageRequest req)
        {
            var senderId = CurrentUser.User!.Id;

            var message = new PrivateMessage(
                content: req.Content,
                senderId: senderId,
                receiverId: req.ReceiverId
            );

            _unitOfWork.PrivateMessages.InsertAsync(message);
            await _unitOfWork.SaveChangesAsync();

            return new NewMessageResponse { Success = true };

        }
    }
}