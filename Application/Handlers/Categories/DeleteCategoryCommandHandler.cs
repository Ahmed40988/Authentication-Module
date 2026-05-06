using Domain.Entities.Categories;
using Domain.Entities.SubSubCategories;
using global::Application.DTO.Categories;

namespace Application.Handlers.Categories
{

        public class DeleteCategoryCommandHandler(
            IGenericRepositories<Category> repo,
            IGenericRepositories<SubCategory> subCategoryRepo,
            IStringLocalizer localizer
        ) : IRequestHandler<DeleteCategoryCommand, Result<bool>>
        {
            private readonly IGenericRepositories<Category> repo = repo;
            private readonly IGenericRepositories<SubCategory> subCategoryRepo = subCategoryRepo;
            private readonly IStringLocalizer localizer = localizer;

            public async Task<Result<bool>> Handle(
                DeleteCategoryCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var category = await repo.GetByGuidIdAsync(
                        request.Id,
                        cancellationToken
                    );

                    if (category is null)
                    {
                        return Result<bool>.Failure(
                            localizer["CategoryNotFound"],
                            404
                        );
                    }

                    var hasSubCategories = await subCategoryRepo.AnyAsync(
                        x => x.CategoryId == request.Id
                    );

                    if (hasSubCategories)
                    {
                        return Result<bool>.Failure(
                            localizer["CannotDeleteLinkedEntity"],
                            400
                        );
                    }

                    await repo.DeleteAsync(category);

                    return Result<bool>.Success(
                        true,
                        localizer["Operationcompletedsuccessfully"]
                    );
                }
                catch (Exception ex)
                {
                    return Result<bool>.Error(
                        $"{localizer["FailedToDeleteCategory"]}: {ex.Message}",
                        500
                    );
                }
            }
        }
    }
