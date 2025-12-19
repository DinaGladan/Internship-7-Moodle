using MoodleSystem.Domain.Common;
using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Enumerations;

public class User : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }

    public ICollection<UserCourse> Enrollments { get; private set; } = new List<UserCourse>();
    public ICollection<PrivateMessage> SentMessages { get; private set; } = new List<PrivateMessage>();
    public ICollection<PrivateMessage> ReceivedMessages { get; private set; } = new List<PrivateMessage>();

    private User() { }

    public User(string firstName, string lastName, string email, string passwordHash)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Role = UserRole.Student;
    }
}
