using MoodleSystem.Console.Helpers;

namespace MoodleSystem.Console.Helpers
{
    public static class MenuNavigator
    {
        public static int Navigate(string title, List<string> options)
        {
            int selected = 0;
            ConsoleKey key;

            do
            {
                System.Console.Clear();
                Writer.WriteHeader(title);
                
                for (int i = 0; i < options.Count(); i++)
                {
                    if (i == selected)
                    {
                        System.Console.ForegroundColor = ConsoleColor.Black;
                        System.Console.BackgroundColor = ConsoleColor.White;
                        Writer.WriteMessage($"> {options[i]}");
                        System.Console.ResetColor();
                    }else
                        Writer.WriteMessage(options[i]);
                }
                key = System.Console.ReadKey(true).Key;

                if (key == System.ConsoleKey.UpArrow)
                    selected = (selected - 1 + options.Count()) % options.Count();
                else if (key == System.ConsoleKey.DownArrow)
                    selected = (selected + 1 + options.Count()) % options.Count();
                else if (key == System.ConsoleKey.Escape)
                    return -1;
            } while(key != System.ConsoleKey.Enter);

            return selected;
        }

    }
}

