using Domain.Entities.SubSubCategories;

namespace Application.Commands.Categories
{
    public record ToggleSubSubCategoryStatusCommand(Guid Id) : IRequest<Result<bool>>;
    public class ToggleSubSubCategoryStatusCommandHandler(
    IGenericRepositories<SubSubCategory> repo,
    IStringLocalizer localizer
    ) : IRequestHandler<ToggleSubSubCategoryStatusCommand, Result<bool>>
    {
        private readonly IGenericRepositories<SubSubCategory> repo = repo;
        private readonly IStringLocalizer localizer = localizer;


        public async Task<Result<bool>> Handle(
            ToggleSubSubCategoryStatusCommand request,
            CancellationToken cancellationToken)
        {
            var subSubCategory = await repo.GetByGuidIdAsync(
                request.Id,
                cancellationToken
            );

            if (subSubCategory is null)
            {
                return Result<bool>.Failure(
                    localizer["SubSubCategoryNotFound"]
                );
            }

            subSubCategory.ToggleStatus();

            await repo.UpdateAsync(
                subSubCategory,
                cancellationToken
            );

            return Result<bool>.Success(
                true,
                localizer["Operationcompletedsuccessfully"]
            );
        }


}

}
