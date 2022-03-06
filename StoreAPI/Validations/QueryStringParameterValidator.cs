using FluentValidation;
using StoreAPI.Dtos;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    public class QueryStringParameterValidator : AbstractValidator<QueryStringParameterDto>
    {
        public QueryStringParameterValidator()
        {
            RuleFor(q => q.PageSize)
                .InclusiveBetween(1, AppConstants.Pagination.MaxPageSize)
                    .WithMessage("{PropertyName} must be between {From} and {To}");

            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1)
                    .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}");
        }
    }
}