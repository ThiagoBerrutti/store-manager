using FluentValidation;
using SalesAPI.Dtos;

namespace SalesAPI.Validations
{
    public class StockValidator : AbstractValidator<ProductStockWriteDto>//, IStockValidator
    {
        public StockValidator()
        {
            RuleFor(s => s.Count)
                .GreaterThanOrEqualTo(0);
            
                
        }
    }
}