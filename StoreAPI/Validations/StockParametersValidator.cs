using FluentValidation;
using StoreAPI.Dtos;
using StoreAPI.Dtos.Shared;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    public class StockParametersValidator : AbstractValidator<StockParametersDto>
    {
        public StockParametersValidator()
        {
            Include(new QueryStringParameterValidator());
                        
            RuleFor(s => s.ProductName)
                .MaximumLength(AppConstants.Validations.Product.NameMaxLength).WithMessage("ProductName maximum lenght is {MaxLength} chars");

            RuleFor(s => s.MinCount)
                .InclusiveBetween(AppConstants.Validations.Stock.CountMinValue, AppConstants.Validations.Stock.CountMaxValue)
                    .WithMessage("MinCount must be between {From} and {To}");

            RuleFor(s => s.MaxCount)
                .InclusiveBetween(AppConstants.Validations.Stock.CountMinValue, AppConstants.Validations.Stock.CountMaxValue)
                    .WithMessage("MaxCount must be between {From} and {To}")
                .GreaterThanOrEqualTo(p => p.MinCount)
                    .WithMessage("MaxCount must be greater or equal to MinCount");
        }
    }
}