using Application.Commands.Categories;
using Application.Common.Helpers;
using Domain.Entities.Categories;
using Domain.Entities.SubCategories;

namespace Application.Handlers.Categories
{
    public class CreateSubCategoryCommandHandler(
        IGenericRepositories<SubCategory> subcategoryrepo,
        IGenericRepositories<Category>categoryrepo,
        IStringLocalizer localizer
    ) : IRequestHandler<CreateSubCategoryCommand, Result<Guid>>
    {
        private readonly IGenericRepositories<SubCategory> subcategoryrepo = subcategoryrepo;
        private readonly IGenericRepositories<Category> categoryrepo = categoryrepo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<Guid>> Handle(
            CreateSubCategoryCommand request,
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

            var exists = await subcategoryrepo.AnyAsync(x =>
                x.Name.En == nameEn ||
                x.Name.Ar == nameAr);

            if (exists)
                return Result<Guid>.Failure(
                    localizer["SubCategoryAlreadyExists"]
                );

            var categoryExists = await categoryrepo.AnyAsync(x => x.Id == dto.CategoryId);
            if (!categoryExists)
                return Result<Guid>.Failure(
                    localizer["CategoryNotFound"]
                );

            var subCategory = new SubCategory(
                nameEn,
                nameAr,
                dto.CategoryId,
                descEn,
                descAr
            );

            await subcategoryrepo.AddAsync(subCategory);
            return Result<Guid>.Success(
                subCategory.Id,
                localizer["Operationcompletedsuccessfully"]
            );
        }
    }
}