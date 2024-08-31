using FluentValidation;

namespace AtulaDotnetTest.Models
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(x => x.Sku)
                .NotEmpty().WithMessage("SKU is required.")
                .Length(3, 10).WithMessage("SKU must be between 3 and 10 characters.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");
        }
    }
}
