namespace Application.Queries.Categories
{
    public record GetSubSubCategoryQuery(Guid Id) : IRequest<Result<SubSubCategoryResponseDetailsDto>>;
}