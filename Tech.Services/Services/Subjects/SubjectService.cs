using Tech.Domain.Entities;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Interfaces.Generics;

namespace Tech.Services.Services.Subjects;

public class SubjectService<TEntity>(IRepository<Subject> repository) : IGettable<TEntity>, IModification<TEntity>
{
	public Task<TEntity> AddAsync(TEntity dto, CancellationToken cancellation = default)
	{
		throw new NotImplementedException();
	}

	public Task<bool> RemoveAsync(long id, CancellationToken cancellation = default)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<TEntity>> RetreiveAllAsync(CancellationToken cancellation = default)
	{
		throw new NotImplementedException();
	}

	public Task<TEntity> RetreiveByIdAsync(long id, CancellationToken cancellation = default)
	{
		throw new NotImplementedException();
	}

	public Task<TEntity> UpdateAsync(long id, TEntity dto, CancellationToken cancellation = default)
	{
		throw new NotImplementedException();
	}
}
