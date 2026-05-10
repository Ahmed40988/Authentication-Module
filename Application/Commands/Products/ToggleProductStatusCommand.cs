namespace Application.Commands.Products;

public record ToggleProductStatusCommand(Guid Id)
    : IRequest<Result<bool>>;
