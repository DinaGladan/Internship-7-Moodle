using MoodleSystem.Application.Common.Model;
using MoodleSystem.Application.DTO;
using MoodleSystem.Domain.Persistence.Users;

namespace MoodleSystem.Application.PrivateMsgs.NewMessage
{
    public class NewMessageUsersRequestHandler
    {
        private readonly IUserRepository _userRepository;

        public NewMessageUsersRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UsersListDTO>> MsgHandler()
        {
            var userId = CurrentUser.User!.Id;

            var users = await _userRepository.GetUsersWithoutChatAsync(userId);

            return users.Select(u => new UsersListDTO
            {
                UserId = u.Id,
                FullName = $"{u.FirstName} {u.LastName}",
                Role = u.Role.ToString()
            }).ToList();
        }
    }

}
