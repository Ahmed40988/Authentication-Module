
using Application.DTO.Products;

namespace Application.Commands.Products;

public record UpdateProductCommand(Guid Id, ProductUpdateRequestDto Dto)
    : IRequest<Result<bool>>;
