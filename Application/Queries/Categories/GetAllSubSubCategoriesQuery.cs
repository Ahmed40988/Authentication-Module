namespace Application.Queries.Categories
{
    public record GetAllSubSubCategoriesQuery(int PageNumber, int PageSize) : IRequest<Result<PaginatedList<SubSubCategoryResponseDto>>>;
}