using AutoMapper;
using Tech.DAL.DTOs.CategoryDTOs;
using Tech.Domain.Entities;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Commons.Exceptions;
using Tech.Services.Interfaces.Generics;

namespace Tech.Services.Services.Categories;

public class CategoryService(
	IRepository<Category> repository,
	IMapper mapper) : IGettable<CategoryDto>
{
	public async Task<IEnumerable<CategoryDto>> RetreiveAllAsync(CancellationToken cancellation = default)
	{
		var categories = await repository.SelectAllAsync(null, null, cancellation);

		if (categories is null)
			throw new CustomException(404, "Category not found!");

		var mapped = mapper.Map<IEnumerable<CategoryDto>>(categories);
		return mapped;
	}

	public async Task<CategoryDto> RetreiveByIdAsync(long id, CancellationToken cancellation = default)
	{
		var category = await repository.SelectAsync(null, null, cancellation);

		if (category is null)
			throw new CustomException(404, "Category not found!");

		var mapped = mapper.Map<CategoryDto>(category);
		return mapped;
	}
}
