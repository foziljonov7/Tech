using FluentValidation;
using Tech.DAL.DTOs.CourseDTOs;

namespace Tech.API.Filters.Courses;

public class CourseForCreateValidator : AbstractValidator<CourseForCreateDto>
{
    public CourseForCreateValidator()
    {
        RuleFor(dto => dto.SubjectId)
            .NotEmpty().WithMessage("SubjectId not empty");

        RuleFor(dto => dto.TeacherId)
            .NotEmpty().WithMessage("TeacherId not empty");

        RuleFor(dto => dto.Price)
            .NotEmpty().WithMessage("Price not empty")
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(dto => dto.StartingDate)
            .Must(BeValidDate).WithMessage("StartingDate must be a valid date");
    }

    private bool BeValidDate(DateTimeOffset? date)
        => date != default(DateTimeOffset);
}
