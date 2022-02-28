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
                .MaximumLength(Math.Min(AppConstants.Validations.User.FirstNameMaxLength, AppConstants.Validations.User.FirstNameMaxLength))
                .WithMessage("Name maximum lenght is {MaxLength} chars");

            RuleFor(ur => ur.UserName)
                .MaximumLength(AppConstants.Validations.User.UsernameMaxLength)
                .WithMessage("UserName length must be between {MinLength} and {MaxLength} chars")
                .When(u => !string.IsNullOrEmpty(u.UserName));

            RuleFor(u => u.MinDateOfBirth)
               .GreaterThanOrEqualTo(new DateTime())
                   .WithMessage("MinDateOfBirth must be after or equal to {PropertyValue:d}");

            RuleFor(u => u.MaxDateOfBirth)
               .LessThanOrEqualTo(DateTime.Today)
                   .WithMessage("MaxDateOfBirth must be before or equal to {ComparisonValue:d}")
                .GreaterThanOrEqualTo(u => u.MinDateOfBirth)
                    .WithMessage("MinDateOfBirth [{ComparisonValue:d}] cannot be later than MaxDateOfBirth [{PropertyValue:d}]");

            RuleFor(u => u.RoleId)
                .GreaterThanOrEqualTo(1).WithMessage("RoleId must be greater than or equal to {ComparisonValue}")
                .When(u => u.RoleId > int.MinValue);
                
        }
    }
}