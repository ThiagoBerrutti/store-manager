using FluentValidation;
using StoreAPI.Dtos;
using StoreAPI.Infra;
using System;

namespace StoreAPI.Validations
{
    public class UserParametersValidator : AbstractValidator<UserParametersDto>
    {
        public UserParametersValidator()
        {
            Include(new QueryStringParameterValidator());

            RuleFor(u => u.Name)
                .MaximumLength(Math.Min(AppConstants.Validations.User.FirstNameMaxLength, AppConstants.Validations.User.LastNameMaxLength))
                .WithMessage("{PropertyName} maximum lenght is {MaxLength} chars");

            RuleFor(ur => ur.UserName)
                .MaximumLength(AppConstants.Validations.User.UsernameMaxLength)
                .WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength} chars")
                .When(u => !string.IsNullOrEmpty(u.UserName));

            RuleFor(u => u.MinDateOfBirth)
               .LessThanOrEqualTo(DateTime.Today)
                   .WithMessage("{PropertyName} must be a date after or equal to {PropertyValue:d}");

            RuleFor(u => u.MaxDateOfBirth)
                .GreaterThanOrEqualTo(u => u.MinDateOfBirth)
                    .When(u => u.MinDateOfBirth.HasValue)
                    .WithMessage("MinDateOfBirth [{ComparisonValue:d}] cannot be a date after {PropertyName} [{PropertyValue:d}]");
              
            RuleFor(u => u.MinAge)
                .GreaterThanOrEqualTo(AppConstants.Validations.User.MinimumAge)
                    .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}");

            RuleFor(u => u.MaxAge)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}")
                .GreaterThanOrEqualTo(u => u.MinAge)
                    .When(u => u.MinAge.HasValue)
                    .WithMessage("{PropertyName} must be greater than or equal to MinAge");

            RuleForEach(u => u.RoleId)
                .GreaterThanOrEqualTo(1).WithMessage("RoleId must be greater than or equal to {ComparisonValue}");                
        }
    }
}