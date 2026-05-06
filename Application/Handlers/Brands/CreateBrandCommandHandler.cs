using Application.Commands.Brands;
using Application.Common.Helpers;

using Domain.Entities.Cataloges;

namespace Application.Handlers.Brands
{
    public class CreateBrandCommandHandler(
        IGenericRepositories<Brand> repo,
        IStringLocalizer localizer,
        IFileStorageService fileStorage
    ) : IRequestHandler<CreateBrandCommand, Result<Guid>>
    {
        private readonly IGenericRepositories<Brand> repo = repo;
        private readonly IStringLocalizer localizer = localizer;
        private readonly IFileStorageService fileStorage = fileStorage;

        public async Task<Result<Guid>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto; 
            var (nameAr, nameEn) = NormalizeHelper.Normalize(dto.Name.AR, dto.Name.EN);
            string? descAr = null;
            string? descEn = null;
            if (dto.Description is not null)
                (descAr, descEn) = NormalizeHelper.Normalize(dto.Description.AR, dto.Description.EN);    

            var exists = await repo.AnyAsync(x => x.Name.En == nameEn || x.Name.Ar == nameAr );
            if (exists)
                return Result<Guid>.Failure(localizer["BrandAlreadyExists"]);

            string? logoImagePath = null;
            if (dto.LogoUrl is not null)
            {
                using var stream = dto.LogoUrl.OpenReadStream();
                logoImagePath = await fileStorage.SaveFileAsync(
                    stream, dto.LogoUrl.FileName,"Brands/logos");
            }

            var brand = new Brand(
                nameEn,
                nameAr,
                descEn,
                descAr,
                logoImagePath
            );

            await repo.AddAsync(brand);

            return Result<Guid>.Success(
                brand.Id,
                localizer["Operationcompletedsuccessfully"]
            );
        }
    }
}
    

