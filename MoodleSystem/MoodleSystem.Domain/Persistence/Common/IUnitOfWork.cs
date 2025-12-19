namespace MoodleSystem.Domain.Persistence.Common
{
    public interface IUnitOfWork
    {
        Task CreateTransactionAsync();
        Task CommitAsync();
        Task RollBack();
        Task SaveChangesAsync();
    }
}
