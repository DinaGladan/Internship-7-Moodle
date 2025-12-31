using MoodleSystem.Application.Admin.ChangeUserRole;
using MoodleSystem.Application.Admin.DeleteUser;
using MoodleSystem.Application.Admin.UpdateUserEmail;
using MoodleSystem.Application.Statistics;
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
        private readonly StatisticsRequestHandler _statisticsRequestHandler;

        public AdminActions(ChangeUserRoleRequestHandler changeUserRoleRequestHandler, UpdateUserEmailAdminRequestHandler updateUserEmailAdminRequestHandler, DeleteUserAdminRequestHandler deleteUserAdminRequestHandler, IUserRepository userRepository, StatisticsRequestHandler statisticsRequestHandler)
        {
            _changeUserRoleRequestHandler = changeUserRoleRequestHandler;
            _updateUserEmailAdminRequestHandler = updateUserEmailAdminRequestHandler;
            _deleteUserAdminRequestHandler = deleteUserAdminRequestHandler;
            _userRepository = userRepository;
            _statisticsRequestHandler = statisticsRequestHandler;
        }

        public async Task<List<User>> GetUsersByRole(UserRole role)
        {
            var users = await _userRepository.GetByRoleAsync(role);
            return users.ToList();
        }

        public async Task<DeleteUserAdminResponse> DeleteUser(int userId)
        {
            var req = new DeleteUserAdminRequest
            {
                UserId = userId
            };
            return await _deleteUserAdminRequestHandler.AdminHandler(req);
        }

        public async Task<UpdateUserEmailAdminResponse>UpdateEmail(int userId, string email)
        {
            var req = new UpdateUserEmailAdminRequest
            {
                UserId = userId,
                UserNewEmail = email
            };

            return await _updateUserEmailAdminRequestHandler.AdminHandler(req);
        }

        public async Task ChangeRole(int userId, UserRole role)
        {
            await _changeUserRoleRequestHandler.AdminHandler(new ChangeUserRoleRequest
            {
                UserId = userId,
                NewRole = role
            });
        }

        public async Task<StatisticsResponse>GetStatistics(DateTime from, DateTime to)
        {
            return await _statisticsRequestHandler.StatisticHandler(from, to);
        }
    }
}