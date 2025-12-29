using MoodleSystem.Application.Common.Model;
using MoodleSystem.Application.Common.Model.LogIn;
using MoodleSystem.Application.Common.Model.Register;
using MoodleSystem.Application.Validation;
using MoodleSystem.Console.Helpers;

namespace MoodleSystem.Console.Actions
{
    public class UserActions
    {
        private readonly LogInRequestHandler _logInRequestHandler;
        private readonly RegisterRequestHandler _registerRequestHandler; 

        public UserActions(LogInRequestHandler logInRequestHandler, RegisterRequestHandler registerRequestHandler)
        {
            _logInRequestHandler = logInRequestHandler;
            _registerRequestHandler = registerRequestHandler;
        }

        public async Task<bool> LogInAsync() {
            Writer.WriteHeader("LOGIN");

            var email = Reader.ReadInput("Email: ");
            var password = Reader.ReadInput("Lozinka: ");

            var req = new LogInRequest { 
                Email = email,
                Password = password
            };

            var response = await _logInRequestHandler.LogInAsync(req);

            if (!response.Success)
            {
                Writer.WriteMessage(response.Message);
                Writer.WaitForKey();
                return false;
            }

            CurrentUser.Set(response.User);
            Writer.WriteMessage($"DEBUG AFTER LOGIN: CurrentUser = {CurrentUser.User?.Email}");
            Writer.WaitForKey();


            Writer.WriteMessage("Uspjesna prijava");
            Writer.WaitForKey();
            return true;
        }
        public async Task RegisterAsync() {
            Writer.WriteHeader("REGISTRACIJA");

            var firstName = Reader.ReadInput("Ime: ");
            var lastName = Reader.ReadInput("Prezime: ");
            var email = Reader.ReadInput("Email: ");
            var password = Reader.ReadInput("Lozinka: ");
            var confirmPassword = Reader.ReadInput("Ponovite Lozinku: ");

            var captchaGenerator = new CreateCaptcha();
            var captchaExpected = captchaGenerator.Generate();

            Writer.WriteMessage($"Captcha: {captchaExpected}");
            var captchaInput = Reader.ReadInput("Upisite Captchu: ");

            var req = new RegisterRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword,
                CaptchaExpected = captchaExpected,
                CaptchaInput = captchaInput
            };

            var response = await _registerRequestHandler.RegisterAsync(req);

            if (!response.Success)
            {
                Writer.WriteMessage(response.Message);
                Writer.WaitForKey();
                return;
            }
            Writer.WriteMessage("Registracija odradjena, sad se mozete prijavit");
            Writer.WaitForKey();
        }
    }
}