using FluentValidation;
using ScaleStore.Core.DTOs.Product;

namespace ScaleStore.Core.Validators.ProductValidators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");

            RuleFor(x => x.Sku)
                .NotEmpty().WithMessage("SKU is required")
                .Matches("^[A-Z0-9-]+$").WithMessage("SKU can only contain uppercase letters, numbers and dashes.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative");
        }
    }
}
