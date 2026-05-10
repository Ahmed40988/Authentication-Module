using Application.Commands.Categories;
using Domain.Entities.SubCategories;

namespace Application.Handlers.Categories
{
        public class ToggleSubCategoryStatusCommandHandler(
            IGenericRepositories<SubCategory> repo,
            IStringLocalizer localizer
        ) : IRequestHandler<ToggleSubCategoryStatusCommand, Result<bool>>
        {
            private readonly IGenericRepositories<SubCategory> repo = repo;
            private readonly IStringLocalizer localizer = localizer;

            public async Task<Result<bool>> Handle(
                ToggleSubCategoryStatusCommand request,
                CancellationToken cancellationToken)
            {
                var subCategory = await repo.GetByGuidIdAsync(
                    request.Id,
                    cancellationToken
                );

                if (subCategory is null)
                {
                    return Result<bool>.Failure(
                        localizer["SubCategoryNotFound"]
                    );
                }

                subCategory.ToggleStatus();

                await repo.UpdateAsync(
                    subCategory,
                    cancellationToken
                );

                return Result<bool>.Success(
                    true,
                    localizer["Operationcompletedsuccessfully"]
                );
            }
        }
    }

