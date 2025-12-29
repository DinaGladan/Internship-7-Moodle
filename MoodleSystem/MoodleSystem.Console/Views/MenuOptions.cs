namespace MoodleSystem.Console.Views
{
    public class MenuOptions
    {
        private readonly Dictionary<string, (string Description, Func<Task<bool>> Action)> _options;

        public MenuOptions()
        {
            _options = [];
        }

        public MenuOptions AddOption(string key, string description, Func<Task<bool>> action)
        {
            _options.Add(key, (description, action));
            return this;
        }

        public Dictionary<string, (string Description, Func<Task<bool>> Action)> Build()
        {
            return _options;
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateMainMenuOptions(MenuManager menuManager)
        {
            return new MenuOptions()
                .AddOption("1", "Prijavi se", async () => { await menuManager.HandleLogIn(); System.Console.Clear(); return false; })
                .AddOption("2", "Registriraj se", async() => { await menuManager.HandleRegister(); System.Console.Clear(); return false; })
                .AddOption("3", "Izlaz", async () => { System.Console.WriteLine("Izlaz iz aplikacije"); return true; })
                .Build();
        }
    }
}