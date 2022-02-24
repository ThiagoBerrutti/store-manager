using FluentValidation;
using StoreAPI.Dtos;
using StoreAPI.Helpers;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidator()
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
        }
    }
}