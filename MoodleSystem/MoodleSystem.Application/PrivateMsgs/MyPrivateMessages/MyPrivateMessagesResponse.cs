using MoodleSystem.Application.DTO;

namespace MoodleSystem.Application.MyPrivateMessages
{
    public class MyPrivateMessagesResponse
    {
        public List<MyPrivateMessagesDTO> PrivateMessages { get; init; } = new();
    }
}
