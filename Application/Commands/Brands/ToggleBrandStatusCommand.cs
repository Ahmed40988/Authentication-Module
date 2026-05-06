namespace Application.Commands.Brands
{
    public record ToggleBrandStatusCommand(Guid Id) : IRequest<Result<bool>>;
}