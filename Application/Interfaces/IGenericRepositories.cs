using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IGenericRepositories<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetByIdAsync(
            Guid id,
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);
        Task<TEntity?> GetByGuidIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> FindAsync(
         Expression<Func<TEntity, bool>> predicate,
         CancellationToken cancellationToken = default);
        IQueryable<TEntity> Query();
        Task AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(ICollection<TEntity> entities);
        Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(
          Expression<Func<TEntity, bool>> predicate,
          CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
