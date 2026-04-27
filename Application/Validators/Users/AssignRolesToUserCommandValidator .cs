using Application.Commands.Users;

namespace Application.Validators.Users
{
        public class AssignRolesToUserValidator : AbstractValidator<AssignRolesToUserCommand>
        {
            public AssignRolesToUserValidator()
            {
                RuleFor(x => x.UserId).NotEmpty();
                RuleFor(x => x.Roles).NotEmpty();
            }
        }
    
}
