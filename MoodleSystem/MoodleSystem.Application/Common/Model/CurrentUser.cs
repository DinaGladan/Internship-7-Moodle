namespace MoodleSystem.Application.Common.Model
{
    public class CurrentUser
    {
        public static User? User { get; set; }

        public static void Set(User user)
        {
            User = user;
        }
        public static void Logout()
        {
            User = null;
        }

        public static bool IsLoggedIn => User != null;
    }
}
