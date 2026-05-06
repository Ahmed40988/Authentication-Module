using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepositories<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task DeleteRangeAsync(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            return entity != null;
        }
        public async Task<bool> ExistsAsync(
          Expression<Func<TEntity, bool>> predicate,
          CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>()
                .AnyAsync(predicate, cancellationToken);
        }
        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellation)
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync(cancellation);
        }
        public async Task<IReadOnlyList<TEntity>> FindAsync(
          Expression<Func<TEntity, bool>> predicate,
          CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public async Task<TEntity?> GetByGuidIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id, cancellationToken);
        }
        public async Task<TEntity?> GetByIdAsync(
        Guid id,
          Expression<Func<TEntity, bool>> predicate,
          CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<TEntity>().Update(entity);
            var affectedRows = await _dbContext.SaveChangesAsync(cancellationToken);
            return affectedRows > 0;
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(predicate);
        }
        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
