using Microsoft.EntityFrameworkCore;
using MoodleSystem.Domain.Common.Model;
using MoodleSystem.Domain.Persistence.Common;
using MoodleSystem.Infrastructure.Persistence;

namespace MoodleSystem.Infrastructure.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {

        private readonly MoodleDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(MoodleDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task<GetAllResponse<TEntity>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return new GetAllResponse<TEntity> { Values = entities };
        }

        public async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await _dbSet.FindAsync(id);

        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Remove(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
