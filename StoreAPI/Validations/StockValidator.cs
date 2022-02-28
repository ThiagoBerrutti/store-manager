using FluentValidation;
using StoreAPI.Dtos;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    public class StockValidator : AbstractValidator<ProductStockWriteDto>
    {
        public StockValidator()
        {
            RuleFor(s => s.Quantity)
                .NotNull()
                    .WithMessage("Quantity cannot be null")
                .InclusiveBetween(AppConstants.Validations.Stock.QuantityMinValue, AppConstants.Validations.Stock.QuantityMaxValue)
                    .WithMessage("Quantity must be between {From} and {To}");
        }
    }
}