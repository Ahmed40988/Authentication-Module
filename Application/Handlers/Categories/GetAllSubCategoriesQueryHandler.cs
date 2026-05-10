using Application.Queries.Categories;
using Domain.Entities.SubCategories;

namespace Application.Handlers.Categories
{
    public class GetAllSubCategoriesQueryHandler(
        IGenericRepositories<SubCategory> repo,
        IStringLocalizer localizer
    ) : IRequestHandler<GetAllSubCategoriesQuery,
        Result<PaginatedList<SubCategoryResponseDto>>>
    {
        private readonly IGenericRepositories<SubCategory> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<PaginatedList<SubCategoryResponseDto>>> Handle(
            GetAllSubCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var query = repo.Query()
                .Include(x => x.Category)
                .Select(subCategory => new SubCategoryResponseDto
                (
                    subCategory.Id,
                   subCategory.CategoryId,
                    new LocalizedDto
                    {
                        EN = subCategory.Name.En,
                        AR = subCategory.Name.Ar
                    },

                    subCategory.Description != null
                        ? new LocalizedDto
                        {
                            EN = subCategory.Description.En,
                            AR = subCategory.Description.Ar
                        }
                        : null,
                    subCategory.IsActive
                ));

            var paginatedSubCategories =
                await PaginatedList<SubCategoryResponseDto>
                .CreateAsync(
                    query,
                    request.PageNumber,
                    request.PageSize
                );

            return Result<PaginatedList<SubCategoryResponseDto>>
                .Success(
                    paginatedSubCategories,
                    localizer["Operationcompletedsuccessfully"]
                );
        }
    }
}