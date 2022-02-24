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
                .InclusiveBetween(1, AppConstants.Pagination.MaxPageSize).WithMessage("PageSize must be between {From} and {To}");

            RuleFor(q => q.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than zero");
        }
    }
}