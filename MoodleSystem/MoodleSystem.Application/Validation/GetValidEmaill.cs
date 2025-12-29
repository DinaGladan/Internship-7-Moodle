using System.Text.RegularExpressions;

namespace MoodleSystem.Application.Validation
{
    public  class GetValidEmail
    {
        public static bool IsEmailValid(string email)
        {
            string pattern = @"^\S+@\S{2,}\.\S{3,}$";
            return Regex.IsMatch( email, pattern);
        }

    }
}
