using Application.Commands.Brands;
using Domain.Entities.Cataloges;

namespace Application.Handlers.Brands
{
    public class ToggleBrandStatusCommandHandler(
        IGenericRepositories<Brand> repo,
        IStringLocalizer localizer
    ) : IRequestHandler<ToggleBrandStatusCommand, Result<bool>>
    {
        private readonly IGenericRepositories<Brand> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<bool>> Handle(ToggleBrandStatusCommand request, CancellationToken cancellationToken)
        {
            var brand = await repo.GetByGuidIdAsync(request.Id, cancellationToken);
            if (brand is null)
                return Result<bool>.Failure(localizer["BrandNotFound"]);

            brand.ToggleStatus();
            await repo.UpdateAsync(brand, cancellationToken);

            return Result<bool>.Success(true, localizer["Operationcompletedsuccessfully"]);
        }
    }
}