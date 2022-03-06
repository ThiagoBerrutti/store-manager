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
                .NotNull()
                    .WithMessage("{PropertyName} cannot be null")
                .NotEmpty()
                    .WithMessage("{PropertyName} cannot be empty")
                .MaximumLength(AppConstants.Validations.Product.NameMaxLength)
                    .WithMessage("{PropertyName} maximum lenght is {MaxLength} chars");

            RuleFor(p => p.Price)
                .NotNull()
                    .WithMessage("{PropertyName} cannot be null")
                .GreaterThanOrEqualTo(AppConstants.Validations.Product.PriceMinValue)
                    .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}");

            RuleFor(p => p.Description)
                .NotNull()
                    .WithMessage("{PropertyName} cannot be null")
                .MaximumLength(AppConstants.Validations.Product.DescriptionMaxLength)
                    .WithMessage("{PropertyName} maximum length is {MaxLength} chars");
        }
    }
}