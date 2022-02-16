using FluentValidation;
using SalesAPI.Dtos;

namespace SalesAPI.Validations
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>//, IUserLoginValidator
    {
        public UserLoginValidator()
        {
            RuleFor(ur => ur.Password)
                .NotNull().WithMessage("'Password' should not be null")
                .MinimumLength(6).WithMessage("'Password' minimum length is 6")
                .MaximumLength(80).WithMessage("'Password' maximum length is 80");

            RuleFor(ur => ur.UserName)
                .NotNull().WithMessage("'UserName' should not be null")
                .MinimumLength(4).WithMessage("'UserName' minimum length is 4")
                .MaximumLength(80).WithMessage("'UserName' maximum length is 80");
        }
    }
}