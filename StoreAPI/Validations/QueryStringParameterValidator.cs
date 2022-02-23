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
                .GreaterThan(0)
                .LessThanOrEqualTo(AppConstants.QueryString.MaxPageSize)
                .WithMessage($"PageSize must be between 1 and {AppConstants.QueryString.MaxPageSize}");



            RuleFor(q => q.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than zero");
        }
    }
}