using MoodleSystem.Application.Common.Model;

namespace MoodleSystem.Application.Common.DashBoard.Professor
{
    public class DashProfessorRequestHandler
    {
        public DashProfessorResponse ProfessorHandler()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("PROFESOR DASHBOARD");
                Console.WriteLine("1. Moji kolegiji\n 2. Privatni chat\n 3.Upravljanje kolegijima\n 4.Odjava");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Moji kolegiji");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.WriteLine("Privatni chat");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.WriteLine("Upravljanje kolegijima");
                        Console.ReadKey();
                        break;
                    case "4":
                        CurrentUser.Logout();
                        return new DashProfessorResponse { LogOut = true };
                    default:
                        Console.WriteLine("Neispravan odabir, Unesite jedan od ponudjenih");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
