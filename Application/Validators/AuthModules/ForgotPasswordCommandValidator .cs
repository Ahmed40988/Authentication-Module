using Applicationtions.Consts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Commands.AuthModules
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(localizer["EmailRequired"])
                .Matches(RegexPatterns.Email)
                .WithMessage(localizer["InvalidEmail"]);
        }
    }
}