namespace Tech.Services.Interfaces.Generics;

public interface IIncludable<TEntity, TInclude>
{
	Task<IEnumerable<TEntity>> RetreiveByIncludesAsync(long id, TInclude include, CancellationToken cancellation = default);
}
