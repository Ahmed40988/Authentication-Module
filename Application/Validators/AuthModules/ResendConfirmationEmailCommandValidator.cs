using Application.Commands.AuthModules;
using Applicationtions.Consts;
using Microsoft.Extensions.Localization;
using ServiceStack.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.AuthModules
{
    public class ResendConfirmationEmailCommandValidator:AbstractValidator<ResendConfirmationEmailCommand>
    {
        public ResendConfirmationEmailCommandValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.Email)
                 .NotEmpty()
                 .WithMessage(localizer["EmailRequired"])
                 .Matches(RegexPatterns.Email)
                 .WithMessage(localizer["InvalidEmail"]);
        }
    }
}
