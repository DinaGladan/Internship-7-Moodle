using MoodleSystem.Domain.Persistence.Courses;
using MoodleSystem.Domain.Persistence.PrivateMessages;
using MoodleSystem.Domain.Persistence.Users;

namespace MoodleSystem.Domain.Persistence.Common
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ICourseRepository Courses { get; }
        IPrivateMessageRepository PrivateMessages { get; }

        Task CreateTransactionAsync();
        Task CommitAsync();
        Task RollBack();
        Task SaveChangesAsync();
    }
}
