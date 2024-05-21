using AutoMapper;
using Tech.DAL.DTOs.CourseDTOs;
using Tech.Domain.Entities;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Commons.Exceptions;
using Tech.Services.Interfaces.Generics;

namespace Tech.Services.Services.Courses;

public class CourseService<CourseDto>(
	IRepository<Course> repository,
	IMapper mapper) : IGettable<CourseDto>, IModification<CourseDto, CourseForCreateDto, CourseForUpdateDto>, IIncludable<CourseDto, string[]>
{
	public async Task<CourseDto> AddAsync(CourseForCreateDto dto, CancellationToken cancellation = default)
	{
		try
		{
			var mapped = mapper.Map<Course>(dto);

			var course = await repository.AddAsync(mapped, cancellation);
			await repository.SaveAsync(cancellation);

			var result = mapper.Map<CourseDto>(course);
			return result;
		}
		catch(Exception ex)
		{
			throw new CustomException(404, "Couln't add course" + ex.InnerException.Message);
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

	public async Task<IEnumerable<CourseDto>> RetreiveAllAsync(CancellationToken cancellation = default)
	{
		var includes = new string[] { "Students" };
		var courses = await repository.SelectAllAsync(null, includes, cancellation);

		if (courses is null)
			throw new CustomException(404, "Course not found!");

		var mapped = mapper.Map<IEnumerable<CourseDto>>(courses);
		return mapped;
	}

	public async Task<CourseDto> RetreiveByIdAsync(long id, CancellationToken cancellation = default)
	{
		var includes = new string[] { "Students" };
		var course = await repository.SelectAsync(null, includes, cancellation);

		if (course is null)
			throw new CustomException(404, "Course not found!");

		var mapped = mapper.Map<CourseDto>(course);
		return mapped;

	}

	public async Task<IEnumerable<CourseDto>> RetreiveByIncludesAsync(long id, string[] include, CancellationToken cancellation = default)
	{
		var courses = await repository.SelectAllAsync(x => x.Students.Any(s => s.CourseId == id), include, cancellation);

		if (courses is null)
			throw new CustomException(404, "Course not found!");

		var mapped = mapper.Map<IEnumerable<CourseDto>>(courses);
		return mapped;
	}

	public async Task<CourseDto> UpdateAsync(long id, CourseForUpdateDto dto, CancellationToken cancellation = default)
	{
		if (!await repository.ExistAsync(id, cancellation))
			throw new CustomException(404, "Id not found!");

		var mapped = mapper.Map<Course>(dto);
		mapped.Id = id;

		var course = await repository.UpdateAsync(mapped, cancellation);
		await repository.SaveAsync(cancellation);

		var result = mapper.Map<CourseDto>(course);

		return result;
	}
}
