using FluentValidation;
using SalesAPI.Dtos;

namespace SalesAPI.Validations
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>//, IChangePasswordValidator
    {
        public ChangePasswordValidator()
        {
            RuleFor(cp => cp.CurrentPassword)
                .NotNull().WithMessage("CurrentPassword should not be null")
                .MinimumLength(6).WithMessage("CurrentPassword minimum length is 6")
                .MaximumLength(80).WithMessage("CurrentPassword maximum length is 80");

            RuleFor(cp => cp.NewPassword)
                .NotNull().WithMessage("NewPassword should not be null")
                .MinimumLength(6).WithMessage("NewPassword minimum length is 6")
                .MaximumLength(80).WithMessage("NewPassword maximum length is 80");                
        }
    }
}