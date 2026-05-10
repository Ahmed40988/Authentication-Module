using Application.DTO.Products;

namespace Application.Commands.Products;

public record CreateProductCommand(ProductRequestDto Dto)
    : IRequest<Result<Guid>>;
