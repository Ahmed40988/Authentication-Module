namespace Application.Commands.AuthModules
{
    public record DeleteRoleCommand(string Id) : IRequest<Result<string>>;
}
