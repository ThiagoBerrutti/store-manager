using FluentValidation;
using FluentValidation.TestHelper;
using StoreAPI.Dtos;

namespace StoreAPI.Validations
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
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

            RuleFor(ur => ur.Password)
                .NotNull().WithMessage("Date of birth should not be null")
                .MinimumLength(4).WithMessage("Password minimum length is 4")
                .MaximumLength(80).WithMessage("Password maximum length is 80");

            RuleFor(ur => ur.UserName)
                .NotNull().WithMessage("UserName should not be null")
                .MinimumLength(4).WithMessage("UserName minimum length is 4")
                .MaximumLength(80).WithMessage("UserName maximum length is 80");
        }
    }
}