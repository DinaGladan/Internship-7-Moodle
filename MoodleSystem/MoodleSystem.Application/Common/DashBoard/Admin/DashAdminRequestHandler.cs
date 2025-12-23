using MoodleSystem.Application.Common.Model;

namespace MoodleSystem.Application.Common.DashBoard.Admin
{
    public class DashAdminRequestHandler
    {
        public DashAdminResponse AdminHandler()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ADMIN DASHBOARD");
                Console.WriteLine("1. Privatni chat\n 2. Upravljanje korisnicima\n 3. Odjava");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Privatni chat");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.WriteLine("Upravljanje korisnicima");
                        Console.ReadKey();
                        break;
                    case "4":
                        CurrentUser.Logout();
                        return new DashAdminResponse { LogOut = true };
                    default:
                        Console.WriteLine("Neispravan odabir, Unesite jedan od ponudjenih");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
