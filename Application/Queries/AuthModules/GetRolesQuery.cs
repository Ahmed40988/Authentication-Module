using Application.DTO.Auth;


namespace Application.Queries.AuthModules
{
    public record GetRolesQuery() : IRequest<Result<List<RoleDto>>>;
}
