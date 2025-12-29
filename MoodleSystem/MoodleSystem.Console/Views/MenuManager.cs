using MoodleSystem.Console.Actions;
using MoodleSystem.Console.Helpers;

namespace MoodleSystem.Console.Views
{
    public class MenuManager
    {
        private readonly UserActions _userActions;

        public MenuManager(UserActions userActions) {  
            _userActions = userActions;
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
        }

        public async Task HandleRegister()
        {
            await _userActions.RegisterAsync();
        }
    }
}
