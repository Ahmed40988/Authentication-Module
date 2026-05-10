using Application.Commands.Categories;

namespace Application.Validators.Categories
{
    public class CreateSubCategoryCommandValidator : AbstractValidator<CreateSubCategoryCommand>
    {
        public CreateSubCategoryCommandValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.Dto.Name)
                .NotNull()
                .WithMessage(localizer["SubCategoryNameRequired"])
                .Must(name =>
                    name != null &&
                    (
                        !string.IsNullOrWhiteSpace(name.AR) ||
                        !string.IsNullOrWhiteSpace(name.EN)
                    )
                )
                .WithMessage(localizer["SubCategoryNameRequired"]);
        }
    }
}