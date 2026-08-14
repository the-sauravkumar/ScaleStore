using FluentValidation;
using ScaleStore.Core.DTOs.Order;

namespace ScaleStore.Core.Validators.OrderValidators
{
    public class UpdateOrderDtoValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderDtoValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("A valid Customer ID is required to place an order.");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("Order total must be greater than zero.");

            RuleFor(x => x.OrderDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Order date cannot be in future.");
        }
    }
}
