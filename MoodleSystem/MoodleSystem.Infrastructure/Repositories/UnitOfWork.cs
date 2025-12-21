using Microsoft.EntityFrameworkCore.Storage;
using MoodleSystem.Domain.Persistence.Common;
using MoodleSystem.Domain.Persistence.Courses;
using MoodleSystem.Domain.Persistence.PrivateMessages;
using MoodleSystem.Domain.Persistence.Users;
using MoodleSystem.Infrastructure.Persistence;

namespace MoodleSystem.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly MoodleDbContext _moodleDb;
        private IDbContextTransaction? _transaction;

        public IUserRepository Users { get; }

        public ICourseRepository Courses { get; }

        public IPrivateMessageRepository PrivateMessages { get; }

        public UnitOfWork(MoodleDbContext moodleDb)
        {
            _moodleDb = moodleDb;
            Users = new UserRepository(moodleDb);
            Courses = new CourseRepository(moodleDb);
            PrivateMessages = new PrivateMessageRepository(moodleDb);
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task CreateTransactionAsync()
        {
            _transaction = await _moodleDb.Database.BeginTransactionAsync();
        }

        public async Task RollBack()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _moodleDb.SaveChangesAsync();
        }
    }
}
