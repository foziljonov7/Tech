namespace Tech.Services.Interfaces.Generics;

public interface IModification<TEntity>
{
	Task<TEntity> AddAsync(TEntity dto, CancellationToken cancellation = default);
	Task<TEntity> UpdateAsync(long id, TEntity dto, CancellationToken cancellation = default);
	Task<bool> RemoveAsync(long id, CancellationToken cancellation = default);
}
