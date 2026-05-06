using Application.Common.Abstractions;
using Application.DTO.Brands;

namespace Application.Queries.Brands
{
    public record GetAllBrandsQuery(
        int PageNumber = 1,
        int PageSize = 10
    ) : IRequest<Result<PaginatedList<BrandsResponseDto>>>;
}