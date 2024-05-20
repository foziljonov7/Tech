namespace Tech.Services.Interfaces.Generics;

public interface IGettable<TEntity>
{
	Task<IEnumerable<TEntity>> RetreiveAllAsync(CancellationToken cancellation = default);
	Task<TEntity> RetreiveByIdAsync(long id, CancellationToken cancellation = default);
}
