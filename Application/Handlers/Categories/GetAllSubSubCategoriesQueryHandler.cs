using Application.Queries.Categories;
using Domain.Entities.SubSubCategories;

namespace Application.Handlers.Categories
{
    public class GetAllSubSubCategoriesQueryHandler(
        IGenericRepositories<SubSubCategory> repo,
        IStringLocalizer localizer
    ) : IRequestHandler<GetAllSubSubCategoriesQuery,
        Result<PaginatedList<SubSubCategoryResponseDto>>>
    {
        private readonly IGenericRepositories<SubSubCategory> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<PaginatedList<SubSubCategoryResponseDto>>> Handle(
            GetAllSubSubCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var query = repo.Query()
                .Include(x => x.SubCategory)
                .Select(subSubCategory => new SubSubCategoryResponseDto
                (
                    subSubCategory.Id,
                    subSubCategory.SubCategoryId,
                    new LocalizedDto
                    {
                        EN = subSubCategory.Name.En,
                        AR = subSubCategory.Name.Ar
                    },

                    subSubCategory.Description != null
                        ? new LocalizedDto
                        {
                            EN = subSubCategory.Description.En,
                            AR = subSubCategory.Description.Ar
                        }
                        : null,
                    subSubCategory.IsActive
                ));

            var paginatedSubSubCategories =
                await PaginatedList<SubSubCategoryResponseDto>
                .CreateAsync(
                    query,
                    request.PageNumber,
                    request.PageSize
                );

            return Result<PaginatedList<SubSubCategoryResponseDto>>
                .Success(
                    paginatedSubSubCategories,
                    localizer["Operationcompletedsuccessfully"]
                );
        }
    }
}