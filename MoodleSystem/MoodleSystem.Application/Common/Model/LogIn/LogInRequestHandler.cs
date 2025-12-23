using MoodleSystem.Domain.Persistence.Users;

namespace MoodleSystem.Application.Common.Model.LogIn
{
    public class LogInRequestHandler
    {
        private readonly IUserRepository _userRepository;

        public LogInRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<LogInResponse> LogInAsync(LogInRequest req)
        {
            var user = await _userRepository.GetByEmailAsync(req.Email);
            if (user == null || user.Password != req.Password)
            {
                await Task.Delay(30000);
                return new LogInResponse
                {
                    Success = false,
                    Message = "Neispravan email ili lozinka"
                };
            }
            return new LogInResponse
            {
                Success = true,
                User = user
            };
        }
    }
}