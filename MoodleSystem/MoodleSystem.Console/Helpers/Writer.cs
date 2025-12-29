namespace MoodleSystem.Console.Helpers
{
    public class Writer
    {
        public static void DisplayMenu(string title, Dictionary<string, (string Description, Func<Task<bool>> Action)> options)
        {
            System.Console.WriteLine($"\n=== {title} ===");

            foreach (var option in options)
            {
                System.Console.WriteLine($"{option.Key}. {option.Value.Description}");
            }
        }
        public static void WriteMessage(string message)
        {
            System.Console.WriteLine(message);
        }

        public static void WaitForKey()
        {
            System.Console.WriteLine("Pritisnite bilo koju tipku za nastavak...");
            System.Console.ReadKey();
        }

        public static void WriteList(IEnumerable<string> items)
        {
            var list = items.ToList();

            if (!list.Any())
            {
                System.Console.WriteLine("Nema podataka");
                return;
            }

            for(int i = 0;i<list.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}. {list[i]}");
            }
        }
    }
}



