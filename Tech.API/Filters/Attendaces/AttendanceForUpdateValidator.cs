using FluentValidation;
using Tech.DAL.DTOs.AttendanceDTOs;

namespace Tech.API.Filters.Attendaces;

public class AttendanceForUpdateValidator : AbstractValidator<AttendanceForUpdateDto>
{
    public AttendanceForUpdateValidator()
    {
        RuleFor(dto => dto.UserId)
            .NotEmpty().WithMessage("UserId not empty");

        RuleFor(dto => dto.CourseId)
            .NotEmpty().WithMessage("CourseId not empty");
    }
}
