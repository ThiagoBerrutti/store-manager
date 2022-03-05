﻿using FluentValidation;
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

            When(p => p.MinPrice.HasValue, () =>
            {
                RuleFor(p => p.MinPrice)
                    .GreaterThanOrEqualTo(0)
                        .When(p => p.MinPrice.HasValue)
                        .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}");
            });
            //.InclusiveBetween(AppConstants.Validations.Product.PriceMinValue, AppConstants.Validations.Product.PriceMaxValue)
            //.WithMessage("Price must be between {From} and {To}");      

            When(p => p.MaxPrice.HasValue, () =>
            {
                RuleFor(p => p.MaxPrice)
                    //.InclusiveBetween(AppConstants.Validations.Product.PriceMinValue, AppConstants.Validations.Product.PriceMaxValue)
                    //    .WithMessage("Price must be between {From} and {To}")
                    .GreaterThanOrEqualTo(p => p.MinPrice)
                        .When(p => p.MaxPrice.HasValue)
                        .WithMessage("MaxPrice must be greater than or equal to MinPrice")
                    .GreaterThanOrEqualTo(0)
                        .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}");
            });


            RuleFor(p => p.Description)
                .MaximumLength(AppConstants.Validations.Product.DescriptionMaxLength)
                    .WithMessage("Description maximum length is {MaxLength} chars");
        }
    }
}