using MoodleSystem.Domain.Abstractions;

namespace MoodleSystem.Domain.Entities
{
    public  class PrivateMessage : BaseEntity
    {
        public string Content { get; set; }
        public User Sender { get; private set; }
        public int SenderId { get; private set; }
        public  User Receiver { get; private set; }
        public int ReceiverId { get; private set; }

        private PrivateMessage() { }
        public PrivateMessage(string content, int senderId, int receiverId) {
            Content = content;
            ReceiverId = receiverId;
            SenderId = senderId;
        }

    }
}
