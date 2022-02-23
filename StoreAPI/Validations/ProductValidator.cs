﻿using FluentValidation;
using StoreAPI.Dtos;

namespace StoreAPI.Validations
{
    public class ProductValidator : AbstractValidator<ProductWriteDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Name cannot be null")
                .NotEmpty().WithMessage("Name cannot be empty")
                .MaximumLength(50).WithMessage("Name maximum lenght is 50");

            RuleFor(p => p.Price)
                .NotNull().WithMessage("Price cannot be null")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater or equal than zero")
                .LessThanOrEqualTo(double.MaxValue).WithMessage($"Price must be less or equal than {double.MaxValue}");

            RuleFor(p => p.Description)
                .NotNull().WithMessage("Description cannot be null")
                .MaximumLength(50).WithMessage("Description maximum length is 50");
        }
    }
}