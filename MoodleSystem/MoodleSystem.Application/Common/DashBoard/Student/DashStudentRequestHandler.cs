using MoodleSystem.Application.Common.DashBoard.Student;
using MoodleSystem.Application.Common.Model;
using System.ComponentModel;

namespace MoodleSystem.Application.Common.DashBoard.Student
{
    public class DashStudentRequestHandler
    {
        public DashStudentResponse StudentHandler()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("STUDENT DASHBOARD");
                Console.WriteLine("1. Moji kolegiji\n 2. Privatni Chat\n 3.Odjava");

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
                        CurrentUser.Logout();
                        return new DashStudentResponse { LogOut = true };
                    default:
                        Console.WriteLine("Neispravan odabir, Unesite jedan od ponudjenih");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
