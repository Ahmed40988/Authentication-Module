using Application.Commands.Categories;
using Domain.Entities.Categories;

namespace Application.Handlers.Categories
{
    public class ToggleCategoryStatusCommandHandler(
        IGenericRepositories<Category> repo,
        IStringLocalizer localizer
    ) : IRequestHandler<ToggleCategoryStatusCommand, Result<bool>>
    {
        private readonly IGenericRepositories<Category> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<bool>> Handle(
            ToggleCategoryStatusCommand request,
            CancellationToken cancellationToken)
        {
            var category = await repo.GetByGuidIdAsync(
                request.Id,
                cancellationToken
            );

            if (category is null)
                return Result<bool>.Failure(
                    localizer["CategoryNotFound"]
                );

            category.ToggleStatus();

            await repo.UpdateAsync(
                category,
                cancellationToken
            );

            return Result<bool>.Success(
                true,
                localizer["Operationcompletedsuccessfully"]
            );
        }
    }
}