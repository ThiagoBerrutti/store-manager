using FluentValidation;
using StoreAPI.Dtos;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    public class StockParametersValidator : AbstractValidator<StockParametersDto>
    {
        public StockParametersValidator()
        {
            Include(new QueryStringParameterValidator());

            RuleFor(s => s.ProductName)
                .MaximumLength(AppConstants.Validations.Product.NameMaxLength)
                    .WithMessage("{PropertyName} maximum lenght is {MaxLength} chars");

            RuleFor(s => s.QuantityMin)
                .GreaterThanOrEqualTo(AppConstants.Validations.Stock.QuantityMinValue)
                    .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}");

            RuleFor(s => s.QuantityMax)
                .GreaterThanOrEqualTo(AppConstants.Validations.Stock.QuantityMinValue)
                    .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}")
                .GreaterThanOrEqualTo(p => p.QuantityMin)
                    .WithMessage("{PropertyName} must be greater than or equal to Quantity Min")
                    .When(s => s.QuantityMin.HasValue);
        }
    }
}