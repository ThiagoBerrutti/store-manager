using FluentValidation;
using StoreAPI.Dtos;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(cp => cp.CurrentPassword)
                .NotNull()
                    .WithMessage("CurrentPassword cannot be null")
                .Length(AppConstants.Validations.User.PasswordMinLength, AppConstants.Validations.User.PasswordMaxLength)
                    .WithMessage("Password length must be between {MinLength} and {MaxLength} chars");

            RuleFor(cp => cp.NewPassword)
                .NotNull()
                    .WithMessage("NewPassword should not be null")
                .Length(AppConstants.Validations.User.PasswordMinLength, AppConstants.Validations.User.PasswordMaxLength)
                    .WithMessage("Password length must be between {MinLength} and {MaxLength} chars");
        }
    }
}