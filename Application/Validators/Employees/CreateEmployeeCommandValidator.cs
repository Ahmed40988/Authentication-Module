using Application.Commands.Employees;
using FluentValidation;

namespace Application.Validators.Employees
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator(IStringLocalizer localizer)
        {

            RuleFor(x => x.Dto.FullName)
                .NotEmpty()
                .WithMessage(localizer["FullNameRequired"]);


            RuleFor(x => x.Dto.PhoneNumber)
                .NotEmpty()
                .WithMessage(localizer["EmployeePhoneRequired"]);

            RuleFor(x => x.Dto.JobTitle)
                .NotEmpty()
                .WithMessage(localizer["JobTitleRequired"]);

            RuleFor(x => x.Dto.DepartmentId)
                .NotEmpty()
                .WithMessage(localizer["DepartmentRequired"]);

            RuleFor(x => x.Dto.HireDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.Dto.HireDate.HasValue)
                .WithMessage(localizer["InvalidDate"]);
       


            RuleFor(x => x.Dto.Email)
              .NotEmpty().WithMessage(localizer["EmailInvalid"])
              .EmailAddress().WithMessage(localizer["Invalidemail"]);

            RuleFor(x => x.Dto.Password)
                .NotEmpty().WithMessage(localizer["Passwordrequired"]);
        }
    }
}
