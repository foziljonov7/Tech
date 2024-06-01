using FluentValidation;
using Tech.DAL.DTOs.SubjectDTOs;
using Tech.Domain.Entities;
using Tech.Infrastructure.Interfaces;

namespace Tech.API.Filters.Subjects;

public class SubjectForUpdateValidator : AbstractValidator<SubjectForCreateDto>
{
    private readonly IRepository<Category> _categoryRepository;
    public SubjectForUpdateValidator(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(dto => dto.Name)
            .NotNull().WithMessage("Name isn't null");

        RuleFor(dto => dto.CategoryId)
            .NotEmpty().WithMessage("CategoryId not empty")
            .MustAsync(CategoryExistsAsync).WithMessage("CategoryId must be a valid category");
    }

    private async Task<bool> CategoryExistsAsync(long categoryId, CancellationToken cancellation = default)
        => await _categoryRepository.ExistAsync(categoryId, cancellation);
}
