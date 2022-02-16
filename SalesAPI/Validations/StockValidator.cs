using FluentValidation;
using SalesAPI.Dtos;

namespace SalesAPI.Validations
{
    public class StockValidator : AbstractValidator<ProductStockWriteDto>
    {
        public StockValidator()
        {
            RuleFor(s => s.Count)
                .NotNull().WithMessage("Count cannot be null")
                .GreaterThanOrEqualTo(0).WithMessage("Count must be greater or equal to zero")
                .LessThanOrEqualTo(int.MaxValue).WithMessage($"Count must be less or equal to {int.MaxValue}");
        }
    }
}