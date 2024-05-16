using System.Linq.Expressions;

namespace Tech.Infrastructure.Interfaces;

public interface IRepository<TEntity>
{
	Task<IQueryable<TEntity>> SelectAllAsync(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, CancellationToken cancellation = default);
	Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null, CancellationToken cancellation = default);
	Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellation = default);
	Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellation = default);
	Task<bool> DeleteAsync(long id, CancellationToken cancellation = default);
	Task<bool> ExistAsync(long id, CancellationToken cancellation = default);
	Task<bool> SaveAsync(CancellationToken cancellation = default);
}
