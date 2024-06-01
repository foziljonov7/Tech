using FluentValidation;
using Tech.DAL.DTOs.PaymentDTOs;

namespace Tech.API.Filters.Payments;

public class PaymentForCourseValidator : AbstractValidator<PaymentForCourseDto>
{
    public PaymentForCourseValidator()
    {
        RuleFor(dto => dto.StudentId)
            .NotEmpty().WithMessage("StudentId not empty");

        RuleFor(dto => dto.CourseId)
            .NotEmpty().WithMessage("CourseId not empty");

        RuleFor(dto => dto.RegistryId)
            .NotEmpty().WithMessage("RegistryId not empty");

        RuleFor(dto => dto.Amount)
            .GreaterThan(0).WithMessage("Payment amount can't be 0")
            .NotEmpty().WithMessage("Payment not empty");
    }
}
