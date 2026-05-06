using Application.Commands.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Brand
{
    public class CreateBrandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.Dto.Name)
                    .NotNull()
                    .WithMessage(localizer["BrandNameRequired"])
                    .Must(name =>
                        name != null &&
                        (
                            !string.IsNullOrWhiteSpace(name.AR) ||
                            !string.IsNullOrWhiteSpace(name.EN)
                        )
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

