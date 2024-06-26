﻿using AutoMapper;
using OfficeOpenXml;
using Tech.DAL.DTOs.CourseDTOs;
using Tech.DAL.DTOs.ExportDTOs;
using Tech.Domain.Entities;
using Tech.Domain.Enums.Courses;
using Tech.Domain.Enums.Users;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Commons.Exceptions;
using Tech.Services.Interfaces.Exports;
using Tech.Services.Interfaces.Generics;

namespace Tech.Services.Services.Courses;

public class CourseService<CourseDto>(
	IRepository<Course> repository,
	IRepository<User> userRepository,
	IMapper mapper) : IGettable<CourseDto>, IModification<CourseDto, CourseForCreateDto, CourseForUpdateDto>,
	IIncludable<CourseDto, string[]>, IExport
{
	public async Task<CourseDto> AddAsync(CourseForCreateDto dto, CancellationToken cancellation = default)
	{
		try
		{
			var mapped = mapper.Map<Course>(dto);

			var teacher = await userRepository.SelectAsync(x => x.Id == mapped.TeacherId, null, cancellation);

			if (teacher.UserRole == Roles.Admin || teacher.UserRole == Roles.Teacher)
			{
				await repository.AddAsync(mapped, cancellation);
				await repository.SaveAsync(cancellation);
			}

			var result = mapper.Map<CourseDto>(mapped);
			return result;
		}
		catch(Exception ex)
		{
			throw new CustomException(404, "Couln't add course" + ex.InnerException.Message);
		}
	}

    public async Task<FileResultDto> ExportToExcelAsync(CancellationToken cancellation = default)
    {
		var data = await repository.SelectAllAsync(x => x.Status == Status.Active, null, cancellation);

		using(var package = new ExcelPackage())
		{
			var workSheet = package.Workbook.Worksheets.Add("Course_sheet1");
			workSheet.Cells.LoadFromCollection(data, true);

			var stream = new MemoryStream();
			package.SaveAs(stream);
			var fileContents = stream.ToArray();

			return new FileResultDto
			{
				Contents = fileContents,
				ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				FileName = "data.xlsx"
			};
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
		var courses = await repository.SelectAllAsync(x => x.Status == Status.Active, includes, cancellation);

		if (courses is null)
			throw new CustomException(404, "Course not found!");

		var mapped = mapper.Map<IEnumerable<CourseDto>>(courses);
		return mapped;
	}

	public async Task<CourseDto> RetreiveByIdAsync(long id, CancellationToken cancellation = default)
	{
		var includes = new string[] { "Students" };
		var course = await repository.SelectAsync(x => x.Id == id, includes, cancellation);

		if (course is null)
			throw new CustomException(404, "Course not found!");

		var mapped = mapper.Map<CourseDto>(course);
		return mapped;

	}

	public async Task<IEnumerable<CourseDto>> RetreiveByIncludesAsync(long id, string[] include, CancellationToken cancellation = default)
	{
		var courses = await repository.SelectAllAsync(x => x.Students.Any(s => s.CourseId == id) && x.Status == Status.Active, include, cancellation);

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
