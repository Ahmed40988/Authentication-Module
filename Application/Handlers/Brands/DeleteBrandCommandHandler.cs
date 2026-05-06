using Application.Commands.Brands;
using Domain.Entities.Cataloges;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Brands
{
        public class DeleteBrandCommandHandler(
            IGenericRepositories<Brand> repo,
            IGenericRepositories<Product> productRepo,
            IStringLocalizer localizer
        ) : IRequestHandler<DeleteBrandCommand, Result<bool>>
        {
        private readonly IGenericRepositories<Brand> repo = repo;
        private readonly IGenericRepositories<Product> productRepo = productRepo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<bool>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            try {
                var brand = await repo.GetByGuidIdAsync(request.Id, cancellationToken);
                if (brand is null)
                    return Result<bool>.Failure(localizer["CommonNotFound"]);
                var hasProducts = await productRepo.AnyAsync(p => p.BrandId != null && p.BrandId == request.Id);

                if (hasProducts)
                    return Result<bool>.Failure(localizer["CannotDeleteLinkedEntity"], 400);

                await repo.DeleteAsync(brand);
                return Result<bool>.Success(true, localizer["Operationcompletedsuccessfully"]);
            }
            
              catch (Exception ex)
            {
                return Result<bool>.Error(
                    $"{localizer["FailedToDeleteBrand"]}: {ex.Message}",
                    500);
            }
}
    }

}
