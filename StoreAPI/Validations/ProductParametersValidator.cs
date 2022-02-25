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
                .MaximumLength(AppConstants.Validations.Product.NameMaxLength).WithMessage("Name maximum lenght is {MaxLength} chars");

            RuleFor(p => p.MinPrice)
                .InclusiveBetween(AppConstants.Validations.Product.PriceMinValue, AppConstants.Validations.Product.PriceMaxValue)
                    .WithMessage("Price must be between {From} and {To}");      

            RuleFor(p => p.MaxPrice)
                .InclusiveBetween(AppConstants.Validations.Product.PriceMinValue, AppConstants.Validations.Product.PriceMaxValue)
                    .WithMessage("Price must be between {From} and {To}")
                .GreaterThanOrEqualTo(p => p.MinPrice)
                    .WithMessage("MaxPrice must be greater or equal to MinPrice");

            RuleFor(p => p.Description)
                .MaximumLength(AppConstants.Validations.Product.DescriptionMaxLength)
                    .WithMessage("Description maximum length is {MaxLength} chars");
        }
    }
}