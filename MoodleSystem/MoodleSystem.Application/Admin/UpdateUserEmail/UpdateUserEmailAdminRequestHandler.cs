using MoodleSystem.Application.Validation;
using MoodleSystem.Domain.Persistence.Common;
using MoodleSystem.Domain.Persistence.Users;

namespace MoodleSystem.Application.Admin.UpdateUserEmail
{
    public class UpdateUserEmailAdminRequestHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UpdateUserEmailAdminRequestHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<UpdateUserEmailAdminResponse> AdminHandler(UpdateUserEmailAdminRequest req)
        {
            var user = await _userRepository.GetByIdAsync(req.UserId);
            if (user == null)
            {
                return new UpdateUserEmailAdminResponse
                {
                    Message = "Korisnik ne postoji",
                    Success = false
                };
            }

            if(await _userRepository.DoesEmailExistsAsync(req.UserNewEmail))
                return new UpdateUserEmailAdminResponse
                {
                    Message = "KorisTaj email vec postoji",
                    Success = false
                };

            if (!GetValidEmail.IsEmailValid(req.UserNewEmail))
            {
                return new UpdateUserEmailAdminResponse
                {
                    Message = "Neispravan format emaila.",
                    Success = false
                };
            }

            user.UpdateUserEmail(req.UserNewEmail);
            await _unitOfWork.SaveChangesAsync();

            return new UpdateUserEmailAdminResponse
            {
                Success = true
            };

        }
    }
}
