using MoodleSystem.Application.DTO;

namespace MoodleSystem.Application.PrivateMsgs.MyPrivateMessages
{
    public class MyPrivateMessagesResponse
    {
        public List<MyPrivateMessagesDTO> PrivateMessages { get; init; } = new();
    }
}
