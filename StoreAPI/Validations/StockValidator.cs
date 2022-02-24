using FluentValidation;
using StoreAPI.Dtos;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    public class StockValidator : AbstractValidator<ProductStockWriteDto>
    {
        public StockValidator()
        {
            RuleFor(s => s.Count)
                .NotNull()
                    .WithMessage("Count cannot be null")
                .InclusiveBetween(AppConstants.Validations.Stock.CountMinValue, AppConstants.Validations.Stock.CountMaxValue)
                    .WithMessage("Count must be between {From} and {To}");
        }
    }
}