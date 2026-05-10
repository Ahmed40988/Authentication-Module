using Application.Commands.Products;

namespace Application.Validators.Products
{
    public class CreateProductValidator
        : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.Dto.Name)
                .NotNull()
                .WithMessage(localizer["ProductNameRequired"])
                .Must(name =>
                    name != null &&
                    (
                        !string.IsNullOrWhiteSpace(name.AR) ||
                        !string.IsNullOrWhiteSpace(name.EN)
                    )
                )
                .WithMessage(localizer["ProductNameRequired"]);

            RuleFor(x => x.Dto.SKU)
                .NotEmpty()
                .WithMessage(localizer["SkuRequired"]);

            RuleFor(x => x.Dto.BrandId)
                .NotEmpty()
                .WithMessage(localizer["BrandIdRequired"]);

            RuleFor(x => x.Dto.CategoryId)
                .NotEmpty()
                .WithMessage(localizer["CategoryIdRequired"]);

            RuleFor(x => x.Dto.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["InvalidPrice"]);

            RuleFor(x => x.Dto.StockQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["InvalidStockQuantity"]);
                 
            RuleFor(x => x.Dto.ImageUrl)
                .Must(BeValidImage)
                .When(x => x.Dto.ImageUrl != null)
                .WithMessage(
                    "Image must be a valid image (jpg, jpeg, png, webp)"
                );
        }

        private bool BeValidImage(IFormFile file)
        {
            var allowedExtensions =
                new[] { ".jpg", ".jpeg", ".png", ".webp" };

            var extension =
                Path.GetExtension(file.FileName).ToLower();

            return allowedExtensions.Contains(extension);
        }
    }
}