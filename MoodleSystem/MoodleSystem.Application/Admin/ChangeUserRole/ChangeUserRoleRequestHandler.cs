using MoodleSystem.Domain.Persistence.Common;
using MoodleSystem.Domain.Persistence.Users;

namespace MoodleSystem.Application.Admin.ChangeUserRole
{
    public class ChangeUserRoleRequestHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public ChangeUserRoleRequestHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<ChangeUserRoleResponse> AdminHandler(ChangeUserRoleRequest req)
        {
            var user = await _userRepository.GetByIdAsync(req.UserId);

            if (user == null)
            {
                return new ChangeUserRoleResponse
                {
                    Message = "Korisnik ne postoji",
                    Success = false
                };
            }
            user.UpdateUserRole(req.NewRole);
            await _unitOfWork.SaveChangesAsync();

            return new ChangeUserRoleResponse
            {
                Success = true
            };
        }

    }
}
