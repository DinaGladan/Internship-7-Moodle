using MoodleSystem.Domain.Persistence.Common;
using MoodleSystem.Domain.Persistence.Users;

namespace MoodleSystem.Application.Admin.DeleteUser
{
    public class DeleteUserAdminRequestHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public DeleteUserAdminRequestHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<DeleteUserAdminResponse> AdminHandler(DeleteUserAdminRequest req)
        {
            var user = await _userRepository.GetByIdAsync(req.UserId);
            if (user == null)
            {
                return new DeleteUserAdminResponse
                {
                    Message = "Korisnik ne postoji",
                    Success = false
                };
            }
            _userRepository.Remove(user);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteUserAdminResponse
            {
                Message = $"Korisnik {user.Id} obrisan",
                Success = true
            };
        }
    }
}