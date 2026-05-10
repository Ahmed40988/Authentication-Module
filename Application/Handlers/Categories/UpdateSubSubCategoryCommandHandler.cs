using Application.Commands.Categories;
using Application.Common.Helpers;
using Domain.Entities.SubCategories;
using Domain.Entities.SubSubCategories;

namespace Application.Handlers.Categories
{
    public class UpdateSubSubCategoryCommandHandler(
        IGenericRepositories<SubSubCategory> subSubCategoryRepo,
        IGenericRepositories<SubCategory> subCategoryRepo,
        IStringLocalizer localizer
    ) : IRequestHandler<UpdateSubSubCategoryCommand, Result<bool>>
    {
        private readonly IGenericRepositories<SubSubCategory> subSubCategoryRepo = subSubCategoryRepo;
        private readonly IGenericRepositories<SubCategory> subCategoryRepo = subCategoryRepo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<bool>> Handle(
            UpdateSubSubCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var subSubCategory = await subSubCategoryRepo.GetByGuidIdAsync(request.Id, cancellationToken);
            if (subSubCategory is null)
                return Result<bool>.Failure(localizer["SubSubCategoryNotFound"], 404);

            if (dto.SubCategoryId.HasValue)
            {
                var subCategoryExists = await subCategoryRepo.AnyAsync(x => x.Id == dto.SubCategoryId);
                if (!subCategoryExists)
                    return Result<bool>.Failure(
                        localizer["SubCategoryNotFound"]
                    );
            }

            var nameAr = string.IsNullOrWhiteSpace(dto.Name?.AR) ? subSubCategory.Name.Ar : dto.Name.AR;
            var nameEn = string.IsNullOrWhiteSpace(dto.Name?.EN) ? subSubCategory.Name.En : dto.Name.EN;
            (nameAr, nameEn) = NormalizeHelper.Normalize(nameAr, nameEn);
            var descAr = string.IsNullOrWhiteSpace(dto.Description?.AR) ? subSubCategory.Description?.Ar : dto.Description.AR;
            var descEn = string.IsNullOrWhiteSpace(dto.Description?.EN)
                ? subSubCategory.Description?.En
                : dto.Description.EN;

            if (!string.IsNullOrWhiteSpace(descAr) ||
                !string.IsNullOrWhiteSpace(descEn))
            {
                (descAr, descEn) = NormalizeHelper.Normalize(
                    descAr,
                    descEn
                );
            }
            var exists = await subSubCategoryRepo.AnyAsync(x =>
               x.Id != request.Id && (x.Name.En == nameEn || x.Name.Ar == nameAr));
            if (exists)
                return Result<bool>.Failure(localizer["SubSubCategoryAlreadyExists"]);

            var result = subSubCategory.Update(nameEn, nameAr, descEn, descAr, dto.SubCategoryId);
            if (!result.IsSuccess)
                return Result<bool>.Failure(result.Message);

            await subSubCategoryRepo.UpdateAsync(subSubCategory, cancellationToken);
             return Result<bool>.Success(true, localizer["Operationcompletedsuccessfully"]);
        }
    }
}