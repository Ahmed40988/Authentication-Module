using Application.Commands.Products;

namespace Application.Validators.Products
{
    public class UpdateProductValidator
        : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator(IStringLocalizer localizer)
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