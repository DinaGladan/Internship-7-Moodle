using MoodleSystem.Domain.Common.Model;

namespace MoodleSystem.Domain.Persistence.Common
{

    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<GetAllResponse<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TId id);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);

    }
}

