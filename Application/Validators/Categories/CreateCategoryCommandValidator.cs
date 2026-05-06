using Application.Commands.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Categories
{
    public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(IStringLocalizer localizer)
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
