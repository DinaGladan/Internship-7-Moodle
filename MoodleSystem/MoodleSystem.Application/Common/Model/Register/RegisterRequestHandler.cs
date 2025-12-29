using MoodleSystem.Application.Validation;
using MoodleSystem.Domain.Persistence.Common;
using MoodleSystem.Domain.Persistence.Users;

namespace MoodleSystem.Application.Common.Model.Register
{
    public class RegisterRequestHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterRequestHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest req)
        {
            if (!GetValidEmail.IsEmailValid(req.Email))
                return new RegisterResponse { Success = false, Message = "Neispravan format emaila." }; 

            if (await _userRepository.DoesEmailExistsAsync(req.Email))
                return new RegisterResponse { Success = false, Message = "Taj email je zauzet." };

            if (req.Password != req.ConfirmPassword)
                return new RegisterResponse { Success = false, Message = "Passwordi se ne poklapaju." };

            if (req.CaptchaInput != req.CaptchaExpected)
                return new RegisterResponse { Success = false, Message = "Captcha nije ispravan." };

            var user = new User(
                firstName: req.FirstName,
                lastName: req.LastName,
                email: req.Email,
                password: req.Password
                );

            await _unitOfWork.Users.InsertAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new RegisterResponse { Success = true};

        }
    }
}