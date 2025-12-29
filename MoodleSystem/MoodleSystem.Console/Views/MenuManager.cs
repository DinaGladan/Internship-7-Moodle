using MoodleSystem.Application.Common.DashBoard;
using MoodleSystem.Application.Common.Model;
using MoodleSystem.Console.Actions;
using MoodleSystem.Console.Helpers;

namespace MoodleSystem.Console.Views
{
    public class MenuManager
    {
        private readonly UserActions _userActions;
        private readonly DashRequestHandler _dashRequestHandler;
        private readonly StudentActions _studentActions;
        private readonly PrivateMessageAction _privateMessageAction;

        public MenuManager(UserActions userActions, DashRequestHandler dashRequestHandler, StudentActions studentActions, PrivateMessageAction privateMessageAction) {  
            _userActions = userActions;
            _dashRequestHandler = dashRequestHandler;
            _studentActions = studentActions;
            _privateMessageAction = privateMessageAction;
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
            var success = await _userActions.LogInAsync();
            if (!success)
                return;

            var dash = _dashRequestHandler.DashHandler();
            
            switch(dash.Type)
            {
                case "Student":
                    await ShowStudentDashboard();
                    return;
                case "Professor":
                    await ShowProfessorDashboard();
                    return;
                case "Admin":
                    await ShowAdminDashboard();
                    return;

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
                var menu = MenuOptions.CreateStudentMenu(this);
                Writer.DisplayMenu("STUDENT IZBORNIK", menu);

                var choice = Reader.ReadMenuChoice();

                if (menu.ContainsKey(choice))
                    logout = await menu[choice].Action();
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

        public async Task ShowStudentCourses()
        {
            Writer.WriteHeader("STUDENT MOJI KOLEGIJI");

            Writer.WriteMessage($"DEBUG CURRENT USER ID = {CurrentUser.User?.Id}");
            Writer.WaitForKey();

            var response = await _studentActions.GetMyCoursesAsync();

            if (!response.Courses.Any())
            {
                Writer.WriteMessage("Nemate niti jedan kolegij. ");
                Writer.WaitForKey();
                return;
            }

            for(int i = 0; i < response.Courses.Count; i++)
            {
                var c = response.Courses[i];
                System.Console.WriteLine($"{i + 1}. {c.Name} Nastavnik: {c.Professor}");
            }
            var input = Reader.ReadInt("\nOdaberite kolegij, za izlaz odaberite 0: ");

            if (!input.HasValue || input.Value == 0)
                return;

            if (input.Value < 1 || input.Value > response.Courses.Count)
            {
                Writer.WriteMessage("Krivi unos.");
                Writer.WaitForKey();
                return;
            }

            var selectedCourse = response.Courses[input.Value - 1];
            await ShowStudentCourseScreen(selectedCourse.CourseId);
        }

        private async Task ShowStudentCourseScreen(int courseId)
        {
            Writer.WriteHeader("KOLEGIJ");

            var response = await _studentActions.GetCourseScreenAsync(courseId);

            Writer.WriteMessage("\nOBAVIJESTI");
            if (!response.Announcements.Any())
                Writer.WriteMessage("Nema obavijesti.");
            else
                foreach (var a in response.Announcements)
                    System.Console.WriteLine($"{a.CreatedAt:g} {a.Title} - {a.Professor}");

            Writer.WriteMessage("\nMATERIJALI");
            if (!response.Materials.Any())
                Writer.WriteMessage("Nema materijala.");
            else
                foreach (var m in response.Materials)
                    System.Console.WriteLine($"{m.CreatedAt:g} {m.Name} {m.Url}");

            Writer.WaitForKey();
        }

        public async Task ShowPrivateMessages()
        {
            bool back = false;
            while (!back)
            {
                Writer.WriteHeader("PRIVATNI CHAT");
                System.Console.WriteLine("1. Nova poruka\n\"2. Moji razgovori\n3. Povratak");
                var choice = Reader.ReadMenuChoice();

                switch (choice)
                {
                    case "1":
                        await ShowNewMessage();
                        break;

                    case "2":
                        await OpenExistingConversations();
                        break;

                    case "3":
                        back = true;
                        break;

                    default:
                        Writer.WriteMessage("Krivi unos.");
                        Writer.WaitForKey();
                        break;
                }
            }
        }

        private async Task OpenExistingConversations()
        {
            var response = await _privateMessageAction.GetMyConversations();

            if (!response.PrivateMessages.Any())
            {
                Writer.WriteMessage("Nemate razgovora");
                Writer.WaitForKey();
                return;
            }
            for (int i = 0; i < response.PrivateMessages.Count; i++)
            {
                var c = response.PrivateMessages[i];
                System.Console.WriteLine($"{i + 1}. {c.FullName}");
            }

            var input = Reader.ReadInt("Odaberite zeljeni razgovor, za izlaz odaberite 0");
            if (!input.HasValue || input.Value == 0)
                return;

            if (input.Value < 1 || input.Value > response.PrivateMessages.Count)
            {
                Writer.WriteMessage("Krivi unos.");
                Writer.WaitForKey();
                return;
            }
            var selected = response.PrivateMessages[input.Value - 1];
            await OpenChatScreen(selected.UserId);
        }

        private async Task OpenChatScreen(int userId)
        {
            bool exit = false;

            while (!exit)
            {
                Writer.WriteHeader("PRIVATE CHAT");

                var chat = await _privateMessageAction.OpenChat(userId);

                foreach (var msg in chat.Conversations)
                {
                    var who = msg.IsMine ? "Ja" : "Druga osoba";
                    System.Console.WriteLine($"{msg.SentAt:g} | {who}: {msg.Content}");
                }

                System.Console.WriteLine("\nUpiši poruku, za izaci upisite /exit:");
                var input = Reader.ReadLine();

                if (input == "/exit")
                    exit = true;
                else
                    await _privateMessageAction.SendMessage(userId, input);
            }
        }

        public async Task ShowNewMessage()
        {
            Writer.WriteHeader("NOVA PORUKA");

            var users = await _privateMessageAction.GetUsersForNewMessage();


            if (!users.Any())
            {
                Writer.WriteMessage("Nemate razgovora.");
                Writer.WaitForKey();
                return;
            }

            for (int i = 0; i < users.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}. {users[i].FullName}");
            }

            var choice = Reader.ReadInt("Odaberite korisnika, 0 za nazad: ");
            if (!choice.HasValue || choice.Value == 0) return;

            if (choice.Value < 1 || choice.Value > users.Count)
            {
                Writer.WriteMessage("Neispravan odabir.");
                Writer.WaitForKey();
                return;
            }

            var selected = users[choice.Value - 1];

            var content = Reader.ReadInput("Poruka: ");
            if (string.IsNullOrWhiteSpace(content))
            {
                Writer.WriteMessage("Poruka ne smije biti prazna.");
                Writer.WaitForKey();
                return;
            }

            await _privateMessageAction.SendMessage(selected.UserId, content);

            Writer.WriteMessage("Poruka poslana.");
            Writer.WaitForKey();
        }

        public async Task ShowProfessorCourses()
        {
            Writer.WriteMessage("jos ne.");
            Writer.WaitForKey();
        }

        public async Task ProfessorCourseManagement()
        {
            Writer.WriteMessage("jos ne");
            Writer.WaitForKey();
        }

        public async Task AdminUsersManagement()
        {
            Writer.WriteMessage("jos ne");
            Writer.WaitForKey();
        }


    }
}