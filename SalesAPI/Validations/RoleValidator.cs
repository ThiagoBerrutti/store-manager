using FluentValidation;
using SalesAPI.Dtos;

namespace SalesAPI.Validations
{
    public class RoleValidator : AbstractValidator<RoleWriteDto>
    {
        public RoleValidator()
        {
            RuleFor(r => r.Name)
                .NotNull().WithMessage("Name cannot be null")
                .NotEmpty().WithMessage("Name cannot be empty")
                .MaximumLength(50).WithMessage("Name maximum lenght is 50");
        }
    }
}