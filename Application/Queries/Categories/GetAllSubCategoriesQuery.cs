using Application.Common.Abstractions;
using Application.DTO.Categories;

namespace Application.Queries.Categories
{
    public record GetAllSubCategoriesQuery(
        int PageNumber = 1,
        int PageSize = 10
    ) : IRequest<Result<PaginatedList<SubCategoryResponseDto>>>;
}