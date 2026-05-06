using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.Dto)
                .NotNull()
                .WithMessage(localizer["BrandDtoRequired"]);

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(localizer["BrandIdRequired"]);

            RuleFor(x => x.Dto.Name)
                .NotNull()
                .WithMessage(localizer["BrandNameRequired"])
                .Must(name =>
                    !string.IsNullOrWhiteSpace(name.AR) ||
                    !string.IsNullOrWhiteSpace(name.EN)
                )
                .WithMessage(localizer["BrandNameRequired"]);


            RuleFor(x => x.Dto.LogoUrl)
                   .Must(BeValidImage)
                   .When(x => x.Dto.LogoUrl != null)
                   .WithMessage("Logo must be a valid image (jpg, png,jpeg,webp)");
        }

        private bool BeValidImage(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            return allowedExtensions.Contains(extension);
        }
    }
    
}