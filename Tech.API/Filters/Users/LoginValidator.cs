using FluentValidation;
using Tech.DAL.DTOs.UserDTOs;

namespace Tech.API.Filters.Users;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(dto => dto.PhoneNumber)
            .NotNull().WithMessage("PhoneNumber isn't null");

        RuleFor(dto => dto.Password)
            .NotNull().WithMessage("Password isn't null");
    }
}
