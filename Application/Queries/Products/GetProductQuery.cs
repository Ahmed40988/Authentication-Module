
using Application.DTO.Products;

namespace Application.Queries.Products;

public record GetProductQuery(Guid Id)
    : IRequest<Result<ProductResponseDetailsDto>>;
