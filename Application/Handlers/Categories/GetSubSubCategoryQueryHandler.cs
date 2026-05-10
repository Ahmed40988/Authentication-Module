using Application.Queries.Categories;
using Domain.Entities.SubSubCategories;

namespace Application.Handlers.Categories
{
    public class GetSubSubCategoryQueryHandler(
        IGenericRepositories<SubSubCategory> repo,
        IStringLocalizer localizer
    ) : IRequestHandler<GetSubSubCategoryQuery,
        Result<SubSubCategoryResponseDetailsDto>>
    {
        private readonly IGenericRepositories<SubSubCategory> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<SubSubCategoryResponseDetailsDto>> Handle(
            GetSubSubCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var subSubCategory = await repo.Query()
                .Include(x => x.SubCategory)
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken
                );

            if (subSubCategory is null)
            {
                return Result<SubSubCategoryResponseDetailsDto>.Failure(
                    localizer["SubSubCategoryNotFound"],
                    404
                );
            }

            var dto = new SubSubCategoryResponseDetailsDto
            (
                subSubCategory.Id,

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

                subSubCategory.SubCategoryId,

                subSubCategory.SubCategory.Name.En,

                subSubCategory.IsActive
            );

            return Result<SubSubCategoryResponseDetailsDto>.Success(
                dto,
                localizer["Operationcompletedsuccessfully"]
            );
        }
    }
}