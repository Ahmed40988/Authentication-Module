using Application.Commands.AuthModules.Application.Commands.AuthModules;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.AuthModules
{
    public class RefreshTokenCommandValidator:AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage(localizer["Refreshtokenrequired"]);
        }
    }
}
