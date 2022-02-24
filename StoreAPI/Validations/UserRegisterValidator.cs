using FluentValidation;
using FluentValidation.TestHelper;
using StoreAPI.Dtos;
using StoreAPI.Helpers;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(u => u.DateOfBirth)
                .NotNull()
                    .WithMessage("DateOfBirth should not be null")
                .Must(date => AgeCalculator.Calculate(date) >= AppConstants.Validations.User.MinimumAge)
                    .WithMessage($"User must be {AppConstants.Validations.User.MinimumAge} or older");

            RuleFor(u => u.FirstName)
                .NotNull()
                    .WithMessage("FirstName should not be null")
                .NotEmpty()
                    .WithMessage("FirstName should not be empty")
                .MaximumLength(AppConstants.Validations.User.FirstNameMaxLength)
                    .WithMessage("FirstName maximum lenght is {MaxLength} chars");

            RuleFor(u => u.LastName)
                .NotNull()
                    .WithMessage("LastName should not be null")
                .MaximumLength(AppConstants.Validations.User.LastNameMaxLength)
                    .WithMessage("LastName maximum lenght is {MaxLength} chars");

            RuleFor(u => u.Password)
                .NotNull()
                    .WithMessage("Date of birth should not be null")
                .Length(AppConstants.Validations.User.PasswordMinLength, AppConstants.Validations.User.PasswordMaxLength)
                    .WithMessage("Password length must be between {MinLength} and {MaxLength} chars");

            RuleFor(u => u.UserName)
                .NotNull()
                    .WithMessage("UserName should not be null")
                .Length(AppConstants.Validations.User.UsernameMinLength, AppConstants.Validations.User.UsernameMaxLength)
                    .WithMessage("UserName length must be between {MinLength} and {MaxLength} chars");
        }
    }
}