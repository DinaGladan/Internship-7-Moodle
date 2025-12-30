using MoodleSystem.Application.Admin.ChangeUserRole;
using MoodleSystem.Application.Admin.DeleteUser;
using MoodleSystem.Application.Admin.UpdateUserEmail;
using MoodleSystem.Domain.Enumerations;
using MoodleSystem.Domain.Persistence.Users;

namespace MoodleSystem.Console.Actions
{
    public class AdminActions
    {
        private readonly ChangeUserRoleRequestHandler _changeUserRoleRequestHandler;
        private readonly UpdateUserEmailAdminRequestHandler _updateUserEmailAdminRequestHandler;
        private readonly DeleteUserAdminRequestHandler _deleteUserAdminRequestHandler;
        private readonly IUserRepository _userRepository;

        public AdminActions(ChangeUserRoleRequestHandler changeUserRoleRequestHandler, UpdateUserEmailAdminRequestHandler updateUserEmailAdminRequestHandler, DeleteUserAdminRequestHandler deleteUserAdminRequestHandler, IUserRepository userRepository)
        {
            _changeUserRoleRequestHandler = changeUserRoleRequestHandler;
            _updateUserEmailAdminRequestHandler = updateUserEmailAdminRequestHandler;
            _deleteUserAdminRequestHandler = deleteUserAdminRequestHandler;
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsersByRole(UserRole role)
        {
            var users = await _userRepository.GetByRoleAsync(role);
            return users.ToList();
        }

        public async Task DeleteUser(int userId)
        {
            await _deleteUserAdminRequestHandler.AdminHandler(new DeleteUserAdminRequest
            {
                UserId = userId
            });
        }

        public async Task UpdateEmail(int userId, string email)
        {
            await _updateUserEmailAdminRequestHandler.AdminHandler(new UpdateUserEmailAdminRequest
            {
                UserId = userId,
                UserNewEmail = email
            });
        }

        public async Task ChangeRole(int userId, UserRole role)
        {
            await _changeUserRoleRequestHandler.AdminHandler(new ChangeUserRoleRequest
            {
                UserId = userId,
                NewRole = role
            });
        }
    }
}