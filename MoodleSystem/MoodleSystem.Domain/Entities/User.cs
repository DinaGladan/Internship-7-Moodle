using MoodleSystem.Domain.Abstractions;
using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Enumerations;

public class User : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; } = null!;
    public string Password { get; private set; }
    public UserRole Role { get; init; }

    public ICollection<UserCourse> Enrollments { get; private set; } = new List<UserCourse>();
    public ICollection<PrivateMessage> SentMessages { get; private set; } = new List<PrivateMessage>();
    public ICollection<PrivateMessage> ReceivedMessages { get; private set; } = new List<PrivateMessage>();

    private User() { }

    public User(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Role = UserRole.Student;
    }
}
