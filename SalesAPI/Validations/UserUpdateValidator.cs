using FluentValidation;
using SalesAPI.Dtos;

namespace SalesAPI.Validations
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>//, IUserUpdateValidator
    {
        public UserUpdateValidator()
        {
            RuleFor(ur => ur.DateOfBirth)
                .NotNull().WithMessage("DateOfBirth should not be null");

            RuleFor(ur => ur.FirstName)
                .NotNull().WithMessage("FirstName should not be null")
                .NotEmpty().WithMessage("FirstName should not be empty")
                .MaximumLength(50).WithMessage("FirstName maximum lenght is 50");

            RuleFor(ur => ur.LastName)
               .NotNull().WithMessage("LastName should not be null")
               .NotEmpty().WithMessage("LastName should not be empty")
               .MaximumLength(50).WithMessage("LastName maximum lenght is 50");
        }
    }
}