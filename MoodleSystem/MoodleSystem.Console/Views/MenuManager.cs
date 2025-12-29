using MoodleSystem.Application.Common.DashBoard;
using MoodleSystem.Console.Actions;
using MoodleSystem.Console.Helpers;
using MoodleSystem.Console.Views;
using System.ComponentModel;

namespace MoodleSystem.Console.Views
{
    public class MenuManager
    {
        private readonly UserActions _userActions;
        private readonly DashRequestHandler _dashRequestHandler;

        public MenuManager(UserActions userActions, DashRequestHandler dashRequestHandler) {  
            _userActions = userActions;
            _dashRequestHandler = dashRequestHandler;
        }

        public async Task RunAsync()
        {
            bool exitRequested = false;

            var mainMenuOptions = MenuOptions.CreateMainMenuOptions(this);

            while (!exitRequested)
            {
                Writer.DisplayMenu("MOODLE SYSTEM - GLAVNI IZBORNIK", mainMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (mainMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await mainMenuOptions[choice].Action();
                }
                else
                {
                    Writer.WriteMessage("Neispravan odabir.");
                    Writer.WaitForKey();
                }
            }
        }

        public async Task HandleLogIn()
        {
            await _userActions.LogInAsync();

            var dash = _dashRequestHandler.DashHandler();
            
            switch(dash.Type)
            {
                case "Student":
                    await ShowStudentDashboard();
                    break;
                case "Professor":
                    await ShowProfessorDashboard();
                    break;
                case "Admin":
                    await ShowAdminDashboard();
                    break;
                default:
                    Writer.WriteMessage("Neuspjesna prijava");
                    Writer.WaitForKey();
                    break;

            }
        }

        public async Task HandleRegister()
        {
            await _userActions.RegisterAsync();
        }

        public async Task ShowStudentDashboard()
        {
            bool logout = false;
            while (!logout)
            {
                Writer.DisplayMenu("STUDENT IZBORNIK", MenuOptions.CreateStudentMenu(this));

                var choice = Reader.ReadMenuChoice();

                if (MenuOptions.CreateStudentMenu(this).ContainsKey(choice))
                    logout = await MenuOptions.CreateStudentMenu(this)[choice].Action();
                else
                    Writer.WriteMessage("Krivi unos");
            }
        }

        public async Task ShowProfessorDashboard()
        {
            bool logout = false;
            while (!logout)
            {
                Writer.DisplayMenu("PROFESOR IZBORNIK", MenuOptions.CreateProfessorMenu(this));

                var choice = Reader.ReadMenuChoice();

                if (MenuOptions.CreateProfessorMenu(this).ContainsKey(choice))
                    logout = await MenuOptions.CreateProfessorMenu(this)[choice].Action();
                else
                    Writer.WriteMessage("Krivi unos");
            }
        }

        public async Task ShowAdminDashboard()
        {
            bool logout = false;
            while (!logout)
            {
                Writer.DisplayMenu("ADMIN IZBORNIK", MenuOptions.CreateAdminMenu(this));

                var choice = Reader.ReadMenuChoice();

                if (MenuOptions.CreateAdminMenu(this).ContainsKey(choice))
                    logout = await MenuOptions.CreateAdminMenu(this)[choice].Action();
                else
                    Writer.WriteMessage("Krivi unos");
            }
        }
    }
}