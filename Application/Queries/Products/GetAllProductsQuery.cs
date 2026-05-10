
using Application.DTO.Products;

namespace Application.Queries.Products;

public record GetAllProductsQuery(
    int PageNumber = 1,
    int PageSize = 10,
    Guid? BrandId = null,
    Guid? CategoryId = null
) : IRequest<Result<PaginatedList<ProductResponseDto>>>;
