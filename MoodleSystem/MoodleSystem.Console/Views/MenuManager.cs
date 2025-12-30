using Microsoft.EntityFrameworkCore.Query;
using MoodleSystem.Application.Common.DashBoard;
using MoodleSystem.Application.Common.Model;
using MoodleSystem.Console.Actions;
using MoodleSystem.Console.Helpers;
using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Enumerations;

namespace MoodleSystem.Console.Views
{
    public class MenuManager
    {
        private readonly UserActions _userActions;
        private readonly DashRequestHandler _dashRequestHandler;
        private readonly StudentActions _studentActions;
        private readonly ProfessorActions _professorActions;
        private readonly PrivateMessageAction _privateMessageAction;
        private readonly AdminActions _adminActions;

        public MenuManager(UserActions userActions, DashRequestHandler dashRequestHandler, StudentActions studentActions, PrivateMessageAction privateMessageAction, ProfessorActions professorActions, AdminActions adminActions)
        {
            _userActions = userActions;
            _dashRequestHandler = dashRequestHandler;
            _studentActions = studentActions;
            _privateMessageAction = privateMessageAction;
            _professorActions = professorActions;
            _adminActions = adminActions;
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

            switch (dash.Type)
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

            var response = await _studentActions.GetMyCoursesAsync();

            if (!response.Courses.Any())
            {
                Writer.WriteMessage("Nemate niti jedan kolegij. ");
                Writer.WaitForKey();
                return;
            }

            for (int i = 0; i < response.Courses.Count; i++)
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
            Writer.WriteHeader("PROFESOR MOJI KOLEGIJI");

            var response = await _professorActions.GetMyCoursesAsync();

            if (!response.Courses.Any())
            {
                Writer.WriteMessage("Nemate niti jedan kolegij. ");
                Writer.WaitForKey();
                return;
            }

            for (int i = 0; i < response.Courses.Count; i++)
            {
                var c = response.Courses[i];
                System.Console.WriteLine($"{i + 1}. {c.Name}");
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
            await ShowProfessorCourseScreen(selectedCourse.CourseId);
        }

        private async Task ShowProfessorCourseScreen(int courseId)
        {

            Writer.WriteHeader("KOLEGIJ");

            var response = await _professorActions.GetCourseScreenAsync(courseId);
            Writer.WriteMessage("STUDENTI");
            if (!response.Students.Any())
            {
                Writer.WriteMessage("Nema upisanih studenata.");
            }
            else
            {
                foreach (var s in response.Students)
                {
                    Writer.WriteMessage($"{s.FirstName} {s.LastName}");
                }
            }

            Writer.WriteMessage("\nOBAVIJESTI");
            if (!response.Announcements.Any())
            {
                Writer.WriteMessage("Nema obavijesti.");
            }
            else
            {
                foreach (var a in response.Announcements)
                {
                    Writer.WriteMessage($"{a.CreatedAt:g} | {a.Title}");
                }
            }

            Writer.WriteMessage("\nMATERIJALI");
            if (!response.Materials.Any())
            {
                Writer.WriteMessage("Nema materijala.");
            }
            else
            {
                foreach (var m in response.Materials)
                {
                    Writer.WriteMessage($"{m.CreatedAt:g} | {m.Name} | {m.Url}");
                }
            }
            Writer.WaitForKey();

        }

        public async Task ProfessorCourseManagement()
        {
            Writer.WriteHeader("UPRAVLJANJE KOLEGIJIMA");

            var response = await _professorActions.GetMyCoursesAsync();

            if (!response.Courses.Any())
            {
                Writer.WriteMessage("Nemate niti jedan kolegij. ");
                Writer.WaitForKey();
                return;
            }

            for (int i = 0; i < response.Courses.Count; i++)
            {
                var c = response.Courses[i];
                System.Console.WriteLine($"{i + 1}. {c.Name}");
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
            await ManagementProfessorCoursesScreen(selectedCourse.CourseId);

            Writer.WaitForKey();
        }

        private async Task ManagementProfessorCoursesScreen(int courseId)
        {
            bool back = false;
            while (!back)
            {
                Writer.WriteHeader("KOLEGIJ MANAGEMENT SCREEN");
                System.Console.WriteLine("1. Dodaj obavijest\n\"2. Dodaj materijal\n3. Dodaj studenta\n4. Povratak");
                var choice = Reader.ReadMenuChoice();

                switch (choice)
                {
                    case "1":
                        await AddNewAnnouncement(courseId);
                        break;

                    case "2":
                        await AddNewMaterial(courseId);
                        break;

                    case "3":
                        await AddNewStudent(courseId);
                        break;

                    case "4":
                        back = true;
                        break;

                    default:
                        Writer.WriteMessage("Krivi unos.");
                        Writer.WaitForKey();
                        break;
                }
            }
        }

        private async Task AddNewAnnouncement(int courseId)
        {
            Writer.WriteHeader("NOVA OBAVIJEST");

            var title = Reader.ReadInput("Naslov: ");
            var content = Reader.ReadInput("Sadrzaj: ");

            var response = await _professorActions.AddAnnouncement(courseId, title, content);

            if (!response.Success)
                Writer.WriteMessage(response.Message);
            else
                Writer.WriteMessage("Obavijest objavljena!");
            Writer.WaitForKey();
        }

        private async Task AddNewMaterial(int courseId)
        {
            Writer.WriteHeader("NOVI MATERIJAL");

            var name = Reader.ReadInput("Naziv: ");
            var url = Reader.ReadInput("Url: ");

            var response = await _professorActions.AddMaterial(courseId, name, url);
            if(!response.Success)
                Writer.WriteMessage(response.Message);
            else
                Writer.WriteMessage("Materijal objavljena!");
            Writer.WaitForKey();
        }

        private async Task AddNewStudent(int courseId)
        {
            Writer.WriteHeader("DODAJ STUDENTA");

            var students = await _professorActions.GetAllStudentsAsync();

            if (!students.Any())
            {
                Writer.WriteMessage("Nemate studenata na ovom kolegiju");
                Writer.WaitForKey();
                return;
            }

            for(int i= 0; i< students.Count; i++)
            {
                Writer.WriteMessage($"{i + 1}. {students[i].FullName}");
            }

            var input = Reader.ReadInt("Odaberite studenta, upisite 0 za nazad.");

            if (!input.HasValue || input.Value == 0)
                return;

            if(input<1 || input.Value > students.Count)
            {
                Writer.WriteMessage("Krivi unos");
                Writer.WaitForKey();
                return;
            }

            var selectedStudent = students[input.Value - 1];

            var response = await _professorActions.AddStudent(courseId,selectedStudent.Id);

            if(!response.Success)
                Writer.WriteMessage(response.Message);
            else
                Writer.WriteMessage("Student dodan na kolegij");
            Writer.WaitForKey();
           
        }

        public async Task AdminUsersManagement()
        {
            bool back = false;

            while(!back)
            {
                Writer.WriteHeader("ADMIN MANAGEMENT SCREEN");
                System.Console.WriteLine("1. Obrisi korisnika\n2. Azuriraj email \n3. Promjeni rolu\n4. Prikazi statistike\n5. Povratak");
                var choice = Reader.ReadMenuChoice();

                switch (choice)
                {
                    case "1":
                        await DeleteUser();
                        break;

                    case "2":
                        await UpdateEmail();
                        break;

                    case "3":
                        await ChangeRole();
                        break;

                    case "4":
                        await ShowStatistics();
                        break;

                    case "5":
                        back = true;
                        break;

                    default:
                        Writer.WriteMessage("Krivi unos.");
                        Writer.WaitForKey();
                        break;
                }
            }
        }

        private async Task DeleteUser()
        {
            Writer.WriteHeader("OBRISI KORISNIKA");
            var students = await _adminActions.GetUsersByRole(UserRole.Student);
            var professors = await _adminActions.GetUsersByRole(UserRole.Professor);
            var users = students.Concat(professors).ToList();

            if (!users.Any())
            {
                Writer.WriteMessage("Nema ni studenat ni profesora");
                Writer.WaitForKey();
                return;
            }

            for(int i = 0; i< users.Count; i++)
            {
                Writer.WriteMessage($"{i + 1}. {users[i].FirstName} {users[i].LastName} - {users[i].Role}");
            }

            var input = Reader.ReadInt("Unesite zeljenog korisnika, za nazad 0");

            if (!input.HasValue || input.Value == 0)
                return;

            var selected = users[input.Value - 1];

            await _adminActions.DeleteUser(selected.Id);
            Writer.WriteMessage("Korisnik obrisan"); //popravi za unos nepravilnog broja, iz DeleteUserAdminRequestHandler
            Writer.WaitForKey();
        }
        private async Task UpdateEmail()
        {
            Writer.WriteHeader("AZURIRANJE EMAILA");
            var students = await _adminActions.GetUsersByRole(UserRole.Student);
            var professors = await _adminActions.GetUsersByRole(UserRole.Professor);
            var users = students.Concat(professors).ToList();

            if (!users.Any())
            {
                Writer.WriteMessage("Nema ni studenat ni profesora");
                Writer.WaitForKey();
                return;
            }

            for (int i = 0; i < users.Count; i++)
            {
                Writer.WriteMessage($"{i + 1}. {users[i].FirstName} {users[i].LastName} - {users[i].Email}");
            }

            var input = Reader.ReadInt("Unesite zeljenog korisnika, za nazad 0");
            if (!input.HasValue || input.Value == 0)
                return;

            var selected = users[input.Value - 1];
            var email = Reader.ReadInput("Unesite novi email: ");

            await _adminActions.UpdateEmail(selected.Id, email);
            Writer.WriteMessage("Email azuriran"); //ne uvik ispisat, ispisat da ne valja ako nije validiran
            Writer.WaitForKey();

        }

        private async Task ChangeRole()
        {
            Writer.WriteHeader("PROMJENA ROLE");
            var students = await _adminActions.GetUsersByRole(UserRole.Student);
            var professors = await _adminActions.GetUsersByRole(UserRole.Professor);
            var users = students.Concat(professors).ToList();

            if (!users.Any())
            {
                Writer.WriteMessage("Nema ni studenat ni profesora");
                Writer.WaitForKey();
                return;
            }

            for (int i = 0; i < users.Count; i++)
            {
                Writer.WriteMessage($"{i + 1}. {users[i].FirstName} {users[i].LastName} - {users[i].Role}");
            }

            var input = Reader.ReadInt("Unesite zeljenog korisnika, za nazad 0");
            if (!input.HasValue || input.Value == 0)
                return;

            var selected = users[input.Value - 1];
            var role = selected.Role == UserRole.Student ? UserRole.Professor : UserRole.Student;

            await _adminActions.ChangeRole(selected.Id, role);
            Writer.WriteMessage("Promijenjena rola");
            Writer.WaitForKey();
        }

        public async Task ShowStatistics()
        {
            bool back = false;
            while (!back)
            {
                Writer.WriteHeader("STATISTIKE");
                System.Console.WriteLine("1. Danas\n2. Ovaj mjesec \n3. Ukupno\n4. Povratak");
                var choice = Reader.ReadMenuChoice();

                DateTime from = DateTime.UtcNow;
                DateTime to = DateTime.UtcNow;

                switch (choice)
                {
                    case "1":
                        from = DateTime.UtcNow.Date;
                        break;

                    case "2":
                        from = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                        break;

                    case "3":
                        from = DateTime.MinValue;
                        break;

                    case "4":
                        back = true;
                        continue;

                    default:
                        Writer.WriteMessage("Krivi unos.");
                        Writer.WaitForKey();
                        break;
                }

                await PrinstStatistics(from, to);
            }

        }

        private async Task PrinstStatistics(DateTime from, DateTime to)
        {
            Writer.WriteHeader("STATISTIKE");
            var statistics = await _adminActions.GetStatistics(from, to);

            Writer.WriteMessage($"Broj korisnika: {statistics.UserCount}");

            Writer.WriteMessage($"Broj kolegija: {statistics.CourseCount}");

            Writer.WriteMessage("\nTop 3 kolegija po broju studenata: ");

            if (!statistics.Top3Courses.Any())
                Writer.WriteMessage("Nema kolegija");
            else
            {
                for(int i = 0;i< statistics.Top3Courses.Count;i++)
                {
                    var course = statistics.Top3Courses[i];
                    Writer.WriteMessage($"{i + 1}. {course.CourseName} - ima {course.StudentCount} studenata");
                }
            }

            Writer.WriteMessage("\nTop 3 studenta po broju poruka: ");
            if (!statistics.Top3Users.Any())
                Writer.WriteMessage("Nema korisnika");
            else
            {
                for(int i = 0; i< statistics.Top3Users.Count; i++)
                {
                    var user = statistics.Top3Users[i];
                    Writer.WriteMessage($"{i + 1}. {user.FullName} - ima {user.MessageCount} poruka");
                }
            }

            Writer.WaitForKey();
        }

    }
}