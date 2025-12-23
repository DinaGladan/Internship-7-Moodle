using MoodleSystem.Application.DTO;
using MoodleSystem.Application.Validation;
using MoodleSystem.Domain.Persistence.Users;

namespace MoodleSystem.Application.Common
{
    public class Authentication
    {
        private readonly IUserRepository _userRepository;

        public Authentication(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User?> LogInAsync(LogInDTO logindto)
        {
            var user = await _userRepository.GetByEmailAsync(logindto.Email);
            if (user == null || user.Password != logindto.Password)
            {
                await Task.Delay(30000);
                return null;
            }
            return user;
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDTO regdto)
        {
            if (!GetValidEmail.IsEmailValid(regdto.Email))
                return (false, "Neisravan format emaila.");

            if (await _userRepository.DoesEmailExistsAsync(regdto.Email))
                return (false, "Taj email je zauzet.");

            if (regdto.Password != regdto.ConfirmPassword)
                return (false, "Passwordi se ne poklapaju. ");

            if (regdto.CaptchaInput != regdto.CaptchaExpected)
                return (false, "Captcha nije ispravan.");

            var user = new User(
                firstName: regdto.FirstName,
                lastName: regdto.LastName,
                email: regdto.Email,
                password: regdto.Password
                );

            await _userRepository.InsertAsync(user);
            return (true, string.Empty);

        }
    }
}
