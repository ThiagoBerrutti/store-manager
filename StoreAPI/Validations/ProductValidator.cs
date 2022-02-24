using FluentValidation;
using StoreAPI.Dtos;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    public class ProductValidator : AbstractValidator<ProductWriteDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Name cannot be null")
                .NotEmpty().WithMessage("Name cannot be empty")
                .MaximumLength(AppConstants.Validations.Product.NameMaxLength).WithMessage("Name maximum lenght is {MaxLength} chars");

            RuleFor(p => p.Price)
                .NotNull().WithMessage("Price cannot be null")
                .InclusiveBetween(AppConstants.Validations.Product.PriceMinValue, AppConstants.Validations.Product.PriceMaxValue)
                .WithMessage("Price must be between {From} and {To}");

            RuleFor(p => p.Description)
                .NotNull().WithMessage("Description cannot be null")
                .MaximumLength(AppConstants.Validations.Product.DescriptionMaxLength).WithMessage("Description maximum length is {MaxLength} chars");
        }
    }
}