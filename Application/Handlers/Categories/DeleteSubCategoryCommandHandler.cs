using Application.Commands.Categories;
using Domain.Entities.SubCategories;
using Domain.Entities.SubSubCategories;

namespace Application.Handlers.Categories
{   
    public class DeleteSubCategoryCommandHandler(
        IGenericRepositories<SubCategory> repo,
        IGenericRepositories<SubSubCategory> subSubCategoryRepo,
        IStringLocalizer localizer
    ) : IRequestHandler<DeleteSubCategoryCommand, Result<bool>>
    {
        private readonly IGenericRepositories<SubCategory> repo = repo;
        private readonly IGenericRepositories<SubSubCategory> subSubCategoryRepo = subSubCategoryRepo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<bool>> Handle(
            DeleteSubCategoryCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var subCategory = await repo.GetByGuidIdAsync(
                    request.Id,
                    cancellationToken
                );

                if (subCategory is null)
                {
                    return Result<bool>.Failure(
                        localizer["SubCategoryNotFound"],
                        404
                    );
                }

                var hasSubSubCategories = await subSubCategoryRepo.AnyAsync(
                    x => x.SubCategoryId == request.Id
                );

                if (hasSubSubCategories)
                {
                    return Result<bool>.Failure(
                        localizer["CannotDeleteLinkedEntity"],
                        400
                    );
                }

                await repo.DeleteAsync(subCategory);

                return Result<bool>.Success(
                    true,
                    localizer["Operationcompletedsuccessfully"]
                );
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(
                    $"{localizer["FailedToDeleteSubCategory"]}: {ex.Message}",
                    500
                );
            }
        }
    }
}