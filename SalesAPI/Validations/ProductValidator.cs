using FluentValidation;
using SalesAPI.Dtos;

namespace SalesAPI.Validations
{
    public class ProductValidator : AbstractValidator<ProductWriteDto>//, IProductValidator
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Name cannot be null")
                .NotEmpty().WithMessage("Name cannot be empty")
                .MaximumLength(50).WithMessage("Name maximum lenght is 50");

            RuleFor(p => p.Price)
                .NotNull().WithMessage("Price cannot be null")
                .NotEmpty().WithMessage("Price cannot be empty")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater or equal than zero")
                .LessThanOrEqualTo(double.MaxValue).WithMessage($"Price must be less or equal than {double.MaxValue}");

            RuleFor(p => p.Description)
                .NotNull().WithMessage("Description cannot be null")
                .MaximumLength(50).WithMessage("Description maximum length is 50");
        }
    }
}