using AutoMapper;
using Tech.DAL.DTOs.SubjectDTOs;
using Tech.Domain.Entities;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Commons.Exceptions;
using Tech.Services.Interfaces.Generics;

namespace Tech.Services.Services.Subjects;

public class SubjectService<TEntity>(
	IRepository<Subject> repository,
	IMapper mapper) : IGettable<SubjectDto>, IModification<SubjectDto, SubjectForCreateDto, SubjectForUpdateDto>, IIncludable<SubjectDto, string[]>
{
	public async Task<SubjectDto> AddAsync(SubjectForCreateDto dto, CancellationToken cancellation = default)
	{
		try
		{
			var mapped = mapper.Map<Subject>(dto);

			var subject = await repository.AddAsync(mapped, cancellation);
			await repository.SaveAsync(cancellation);

			var result = mapper.Map<SubjectDto>(subject);
			return result;
		}
		catch(Exception ex)
		{
			throw new CustomException(404, "Subject not found!" + ex.InnerException.Message);
		}
	}

	public async Task<bool> RemoveAsync(long id, CancellationToken cancellation = default)
	{
		if (!await repository.ExistAsync(id, cancellation))
			return false;

		await repository.DeleteAsync(id, cancellation);
		await repository.SaveAsync(cancellation);

		return true;
	}

	public async Task<IEnumerable<SubjectDto>> RetreiveAllAsync(CancellationToken cancellation = default)
	{
		var includes = new string[] { "Students" };
		var subjects = await repository.SelectAllAsync(null, includes, cancellation);

		if (subjects is null)
			throw new CustomException(404, "Subject not found!");

		var mapped = mapper.Map<IEnumerable<SubjectDto>>(subjects);
		return mapped;
	}

	public async Task<SubjectDto> RetreiveByIdAsync(long id, CancellationToken cancellation = default)
	{
		var includes = new string[] { "Students" };
		var subject = await repository.SelectAsync(null, includes, cancellation);

		if (subject is null)
			throw new CustomException(404, "Subject not found!");

		var mapped = mapper.Map<SubjectDto>(subject);
		return mapped;
	}

	public async Task<IEnumerable<SubjectDto>> RetreiveByIncludesAsync(long id, string[] include, CancellationToken cancellation = default)
	{
		var subjects = await repository.SelectAllAsync(x => x.CategoryId == id, include, cancellation);

		if (subjects is null)
			throw new CustomException(404, "Subject not found!");

		var mapped = mapper.Map<IEnumerable<SubjectDto>>(subjects);
		return mapped;
	}

	public async Task<SubjectDto> UpdateAsync(long id, SubjectForUpdateDto dto, CancellationToken cancellation = default)
	{
		if (!await repository.ExistAsync(id, cancellation))
			throw new CustomException(404, "Subject not found!");

		var mapped = mapper.Map<Subject>(dto);
		mapped.Id = id;

		var subject = await repository.UpdateAsync(mapped, cancellation);
		var result = mapper.Map<SubjectDto>(subject);

		return result;
	}
}
