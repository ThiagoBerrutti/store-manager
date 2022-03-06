using FluentValidation;
using StoreAPI.Dtos;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    public class ProductParametersValidator : AbstractValidator<ProductParametersDto>
    {
        public ProductParametersValidator()
        {
            Include(new QueryStringParameterValidator());

            RuleFor(p => p.Name)
                .MaximumLength(AppConstants.Validations.Product.NameMaxLength)
                    .WithMessage("Name maximum lenght is {MaxLength} chars");

            RuleFor(p => p.MinPrice)
                 .GreaterThanOrEqualTo(AppConstants.Validations.Product.PriceMinValue)
                    .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}")
                .LessThanOrEqualTo(AppConstants.Validations.Product.PriceMaxValue)
                     .WithMessage("{PropertyName} must be less than or equal to {ComparisonValue}");

            RuleFor(p => p.MaxPrice)
                .GreaterThanOrEqualTo(p => p.MinPrice)
                    .When(p => p.MinPrice.HasValue)
                    .WithMessage("MaxPrice must be greater than or equal to MinPrice")
                .GreaterThanOrEqualTo(AppConstants.Validations.Product.PriceMinValue)
                    .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}")
                .LessThanOrEqualTo(AppConstants.Validations.Product.PriceMaxValue)
                     .WithMessage("{PropertyName} must be less than or equal to {ComparisonValue}");

            RuleFor(p => p.Description)
                .MaximumLength(AppConstants.Validations.Product.DescriptionMaxLength)
                    .WithMessage("Description maximum length is {MaxLength} chars");
        }
    }
}