using Application.Commands.Categories;

namespace Application.Validators.Categories
{
    public class CreateSubSubCategoryCommandValidator : AbstractValidator<CreateSubSubCategoryCommand>
    {
        public CreateSubSubCategoryCommandValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.Dto.Name)
                .NotNull()
                .WithMessage(localizer["SubSubCategoryNameRequired"])
                .Must(name =>
                    name != null &&
                    (
                        !string.IsNullOrWhiteSpace(name.AR) ||
                        !string.IsNullOrWhiteSpace(name.EN)
                    )
                )
                .WithMessage(localizer["SubSubCategoryNameRequired"]);
        }
    }
}