using Application.Common.Helpers;
using Domain.Entities.Cataloges;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandHandler(
        IGenericRepositories<Brand> repo,
        IStringLocalizer localizer,
        IFileStorageService fileStorage
    ) : IRequestHandler<UpdateBrandCommand, Result<bool>>
    {
        private readonly IGenericRepositories<Brand> repo = repo;
        private readonly IStringLocalizer localizer = localizer;
        private readonly IFileStorageService fileStorage = fileStorage;

        public async Task<Result<bool>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var brand = await repo.GetByGuidIdAsync(request.Id, cancellationToken);
            if (brand is null)
                return Result<bool>.Failure(localizer["BrandNotFound"]);
            var (nameAr, nameEn) = NormalizeHelper.Normalize(dto.Name.AR, dto.Name.EN);
            string? descAr = null;
            string? descEn = null;

            if (dto.Description is not null)
                (descAr, descEn) = NormalizeHelper.Normalize(dto.Description.AR, dto.Description.EN);

            var exists = await repo.AnyAsync(x => (x.Name.En == nameEn || x.Name.Ar == nameAr) && x.Id != request.Id );

            if (exists)
                return Result<bool>.Failure(localizer["BrandAlreadyExists"]);

            string? logoPath = brand.LogoUrl;

            if (dto.LogoUrl is not null)
            {
                using var stream = dto.LogoUrl.OpenReadStream();
                logoPath = await fileStorage.SaveFileAsync(
                    stream,
                    dto.LogoUrl.FileName,
                    "Brands/logos"
                );
            }
            var updateResult = brand.Update(
                nameEn,
                nameAr,
                descEn,
                descAr,
                logoPath
            );

            if (!updateResult.IsSuccess)
                return Result<bool>.Failure(localizer[updateResult.Message]);

            await repo.UpdateAsync(brand, cancellationToken);
            return Result<bool>.Success(true, localizer["Operationcompletedsuccessfully"]);
        }
    }
}