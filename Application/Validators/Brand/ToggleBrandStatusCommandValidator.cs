using Application.Commands.Brands;

namespace Application.Validators.Brand
{
    public class ToggleBrandStatusCommandValidator : AbstractValidator<ToggleBrandStatusCommand>
    {
        public ToggleBrandStatusCommandValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(localizer["BrandIdRequired"]);
        }
    }
}