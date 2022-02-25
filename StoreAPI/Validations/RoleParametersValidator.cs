using FluentValidation;
using StoreAPI.Dtos;
using StoreAPI.Dtos.Shared;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    public class RoleParametersValidator : AbstractValidator<RoleParametersDto>
    {
        public RoleParametersValidator()
        {
            Include(new QueryStringParameterValidator());
                        
            RuleFor(r => r.Name)
                .MaximumLength(AppConstants.Validations.Product.NameMaxLength).WithMessage("Name maximum lenght is {MaxLength} chars");
        }
    }
}