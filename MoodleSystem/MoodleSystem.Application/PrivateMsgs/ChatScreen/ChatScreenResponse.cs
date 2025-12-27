using MoodleSystem.Application.DTO;

namespace MoodleSystem.Application.ChatScreen
{
    public class ChatScreenResponse
    {
        public List<ChatScreenDTO> Conversations { get; init; } = new();
    }
}
