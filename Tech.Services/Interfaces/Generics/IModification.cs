namespace Tech.Services.Interfaces.Generics;

public interface IModification<TEntity, TCreate, TUpdate>
{
	Task<TEntity> AddAsync(TCreate dto, CancellationToken cancellation = default);
	Task<TEntity> UpdateAsync(long id, TUpdate dto, CancellationToken cancellation = default);
	Task<bool> RemoveAsync(long id, CancellationToken cancellation = default);
}
