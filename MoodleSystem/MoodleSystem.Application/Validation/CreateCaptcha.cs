namespace MoodleSystem.Application.Validation
{
    public class CreateCaptcha
    {
        public string Generate()
        {
            const string letters = "ABCDEFGHIJKLMNOPRSTUVZQWY";
            const string numbers = "0123456789";

            var random = new Random();
            var letter = letters[random.Next(letters.Length)];
            var number = numbers[random.Next(numbers.Length)];

            return $"{letter}{number}{random.Next(10, 20)}";
        }
    }
}
