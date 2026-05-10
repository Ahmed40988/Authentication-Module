using Application.Queries.Categories;
using Domain.Entities.SubCategories;

namespace Application.Handlers.Categories
{
    public class GetSubCategoryQueryHandler(
        IGenericRepositories<SubCategory> repo,
        IStringLocalizer localizer
    ) : IRequestHandler<GetSubCategoryQuery,
        Result<SubCategoryResponseDetailsDto>>
    {
        private readonly IGenericRepositories<SubCategory> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<SubCategoryResponseDetailsDto>> Handle(
            GetSubCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var subCategory = await repo.Query()
                .Include(x => x.Category)
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken
                );

            if (subCategory is null)
            {
                return Result<SubCategoryResponseDetailsDto>.Failure(
                    localizer["SubCategoryNotFound"],
                    404
                );
            }

            var dto = new SubCategoryResponseDetailsDto
            (
                subCategory.Id,

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

                subCategory.CategoryId,

                subCategory.Category.Name.En,

                subCategory.IsActive
            );

            return Result<SubCategoryResponseDetailsDto>.Success(
                dto,
                localizer["Operationcompletedsuccessfully"]
            );
        }
    }
}