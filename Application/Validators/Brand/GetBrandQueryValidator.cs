using Application.Commands.Brands;
using Application.Queries.Brands;

namespace Application.Validators.Brand
{
    public class GetBrandQueryValidator : AbstractValidator<GetBrandQuery>
    {
        public GetBrandQueryValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(localizer["BrandIdRequired"]);
        }
    }
}