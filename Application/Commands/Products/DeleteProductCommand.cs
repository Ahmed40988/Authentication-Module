namespace Application.Commands.Products;

public record DeleteProductCommand(Guid Id)
    : IRequest<Result<bool>>;
