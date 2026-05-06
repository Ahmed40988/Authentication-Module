using Application.Commands.Categories;

namespace Application.Validators.Categories
{
    public class UpdateCategoryCommandValidator:AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.Dto.Name)
                 .NotNull()
                 .WithMessage(localizer["CategoryNameRequired"])
                 .Must(name =>
                     name != null &&
                     (
                         !string.IsNullOrWhiteSpace(name.AR) ||
                         !string.IsNullOrWhiteSpace(name.EN)
                     )
                 )
                 .WithMessage(localizer["CategoryNameRequired"]);
        
    }
    }
}
