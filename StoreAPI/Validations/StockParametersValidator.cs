﻿using FluentValidation;
using StoreAPI.Dtos;
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

            RuleFor(s => s.QuantityMin)
                .InclusiveBetween(AppConstants.Validations.Stock.QuantityMinValue, AppConstants.Validations.Stock.QuantityMaxValue)
                    .WithMessage("QuantityMin must be between {From} and {To}");

            RuleFor(s => s.QuantityMax)
                .InclusiveBetween(AppConstants.Validations.Stock.QuantityMinValue, AppConstants.Validations.Stock.QuantityMaxValue)
                    .WithMessage("QuantityMax must be between {From} and {To}")
                .GreaterThanOrEqualTo(p => p.QuantityMin)
                    .WithMessage("QuantityMax must be greater than or equal to QuantityMin");
        }
    }
}