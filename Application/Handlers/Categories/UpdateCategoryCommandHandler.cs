using Application.Commands.Categories;
using Application.Common.Helpers;
using Domain.Entities.Categories;

namespace Application.Handlers.Categories
{
        public class UpdateCategoryCommandHandler(
            IGenericRepositories<Category> repo,
            IStringLocalizer localizer
        ) : IRequestHandler<UpdateCategoryCommand, Result<bool>>
        {
            private readonly IGenericRepositories<Category> repo = repo;
            private readonly IStringLocalizer localizer = localizer;

            public async Task<Result<bool>> Handle(
                UpdateCategoryCommand request,
                CancellationToken cancellationToken)
            {
                var dto = request.Dto;
                var category = await repo.GetByGuidIdAsync(request.Id,  cancellationToken );
                if (category is null)
                    return Result<bool>.Failure(localizer["CategoryNotFound"], 404);
                

            var nameAr = string.IsNullOrWhiteSpace(dto.Name?.AR)? category.Name.Ar : dto.Name.AR;

            var nameEn = string.IsNullOrWhiteSpace(dto.Name?.EN) ? category.Name.En : dto.Name.EN;

            (nameAr, nameEn) = NormalizeHelper.Normalize( nameAr,  nameEn );

            var descAr = string.IsNullOrWhiteSpace(dto.Description?.AR)  ? category.Description?.Ar : dto.Description.AR;

            var descEn = string.IsNullOrWhiteSpace(dto.Description?.EN)
                ? category.Description?.En
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
                    x.Id != request.Id &&( x.Name.En == nameEn || x.Name.Ar == nameAr));
                if (exists)
                    return Result<bool>.Failure(localizer["CategoryAlreadyExists"] );

                var result = category.Update(nameEn, nameAr, descEn, descAr);
                if (!result.IsSuccess)
                return Result<bool>.Failure(result.Message);

                await repo.UpdateAsync(category, cancellationToken);

                return Result<bool>.Success(true, localizer["Operationcompletedsuccessfully"] );
            }
        }
    }
