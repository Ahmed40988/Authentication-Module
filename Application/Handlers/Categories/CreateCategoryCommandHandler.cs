using Application.Commands.Categories;
using Application.Common.Helpers;
using Domain.Entities.Categories;

namespace Application.Handlers.Categories
{
    public class CreateCategoryCommandHandler(
        IGenericRepositories<Category> repo,
        IStringLocalizer localizer
    ) : IRequestHandler<CreateCategoryCommand, Result<Guid>>
    {
        private readonly IGenericRepositories<Category> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<Guid>> Handle(
            CreateCategoryCommand request,
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

            var exists = await repo.AnyAsync(x =>
                x.Name.En == nameEn ||
                x.Name.Ar == nameAr );

            if (exists)
                return Result<Guid>.Failure(
                    localizer["CategoryAlreadyExists"]
                );

            var category = new Category(
                nameEn,
                nameAr,
                descEn,
                descAr
            );

            await repo.AddAsync(category);
            return Result<Guid>.Success(
                category.Id,
                localizer["Operationcompletedsuccessfully"]
            );
        }
    }
}