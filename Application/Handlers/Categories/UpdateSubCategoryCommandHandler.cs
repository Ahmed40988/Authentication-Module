using Application.Commands.Categories;
using Application.Common.Helpers;
using Domain.Entities.Categories;
using Domain.Entities.SubCategories;

namespace Application.Handlers.Categories
{
    public class UpdateSubCategoryCommandHandler(IGenericRepositories<Category> categoryrepo, IGenericRepositories<SubCategory> repo,IStringLocalizer localizer) : IRequestHandler<UpdateSubCategoryCommand, Result<bool>>
    {
        private readonly IGenericRepositories<Category> categoryrepo = categoryrepo;
        private readonly IGenericRepositories<SubCategory> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<bool>> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var subCategory = await repo.GetByGuidIdAsync(request.Id, cancellationToken);
            if(subCategory is null)
                return Result<bool>.Failure(localizer["SubCategoryNotFound"], 404);

            if (dto.CategoryId.HasValue)
            {
                var categoryExists = await categoryrepo.AnyAsync(x => x.Id == dto.CategoryId);
                if (!categoryExists)
                    return Result<bool>.Failure(
                        localizer["CategoryNotFound"]
                    );
            }

            var nameAr = string.IsNullOrWhiteSpace(dto.Name?.AR) ? subCategory.Name.Ar : dto.Name.AR;
            var nameEn = string.IsNullOrWhiteSpace(dto.Name?.EN) ? subCategory.Name.En : dto.Name.EN;
            (nameAr, nameEn) = NormalizeHelper.Normalize(nameAr, nameEn);
            var descAr = string.IsNullOrWhiteSpace(dto.Description?.AR) ? subCategory.Description?.Ar : dto.Description.AR;
            var descEn = string.IsNullOrWhiteSpace(dto.Description?.EN)
                ? subCategory.Description?.En
                : dto.Description.EN;

            if (!string.IsNullOrWhiteSpace(descAr) ||
                !string.IsNullOrWhiteSpace(descEn))
            {
                (descAr, descEn) = NormalizeHelper.Normalize(
                    descAr,
                    descEn
                );
            }
            var exists = await repo.AnyAsync(x =>
               x.Id != request.Id && (x.Name.En == nameEn || x.Name.Ar == nameAr));
            if (exists)
                return Result<bool>.Failure(localizer["SubCategoryAlreadyExists"]);

            var result = subCategory.Update(nameEn, nameAr, descEn, descAr,dto.CategoryId);
            if (!result.IsSuccess)
                return Result<bool>.Failure(result.Message);

            await repo.UpdateAsync(subCategory, cancellationToken);
             return Result<bool>.Success(true, localizer["Operationcompletedsuccessfully"]);
        }
    }
}
