using FluentValidation;
using ScaleStore.Core.DTOs.Customer;

namespace ScaleStore.Core.Validators.CustomerValidators
{
    public class UpdateCustomerDtoValidator : AbstractValidator<UpdateCustomerDto>
    {
        public UpdateCustomerDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");
        }
    }
}
