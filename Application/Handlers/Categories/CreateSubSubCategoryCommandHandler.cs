using Application.Commands.Categories;
using Application.Common.Helpers;
using Domain.Entities.SubCategories;
using Domain.Entities.SubSubCategories;

namespace Application.Handlers.Categories
{
    public class CreateSubSubCategoryCommandHandler(
        IGenericRepositories<SubSubCategory> subSubCategoryRepo,
        IGenericRepositories<SubCategory> subCategoryRepo,
        IStringLocalizer localizer
    ) : IRequestHandler<CreateSubSubCategoryCommand, Result<Guid>>
    {
        private readonly IGenericRepositories<SubSubCategory> subSubCategoryRepo = subSubCategoryRepo;
        private readonly IGenericRepositories<SubCategory> subCategoryRepo = subCategoryRepo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<Guid>> Handle(
            CreateSubSubCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var (nameAr, nameEn) = NormalizeHelper.Normalize(
                dto.Name.AR,
                dto.Name.EN
            );

            string? descAr = null;
            string? descEn = null;

            if (dto.Description is not null)
            {
                (descAr, descEn) = NormalizeHelper.Normalize(
                    dto.Description.AR,
                    dto.Description.EN
                );
            }

            var exists = await subSubCategoryRepo.AnyAsync(x =>
                x.Name.En == nameEn ||
                x.Name.Ar == nameAr);

            if (exists)
                return Result<Guid>.Failure(
                    localizer["SubSubCategoryAlreadyExists"]
                );

            var subCategoryExists = await subCategoryRepo.AnyAsync(x => x.Id == dto.SubCategoryId);
            if (!subCategoryExists)
                return Result<Guid>.Failure(
                    localizer["SubCategoryNotFound"]
                );

            var subSubCategory = new SubSubCategory(
                nameEn,
                nameAr,
                dto.SubCategoryId,
                descEn,
                descAr
            );

            await subSubCategoryRepo.AddAsync(subSubCategory);
            return Result<Guid>.Success(
                subSubCategory.Id,
                localizer["Operationcompletedsuccessfully"]
            );
        }
    }
}