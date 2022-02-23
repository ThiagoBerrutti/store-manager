using FluentValidation;
using StoreAPI.Dtos;

namespace StoreAPI.Validations
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(cp => cp.CurrentPassword)
                .NotNull().WithMessage("CurrentPassword should not be null")
                .MinimumLength(4).WithMessage("CurrentPassword minimum length is 4")
                .MaximumLength(80).WithMessage("CurrentPassword maximum length is 80");

            RuleFor(cp => cp.NewPassword)
                .NotNull().WithMessage("NewPassword should not be null")
                .MinimumLength(4).WithMessage("NewPassword minimum length is 4")
                .MaximumLength(80).WithMessage("NewPassword maximum length is 80");                
        }
    }
}