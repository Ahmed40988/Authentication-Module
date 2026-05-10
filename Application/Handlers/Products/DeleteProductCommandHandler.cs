
using Application.Commands.Products;
using Domain.Entities.Products;

namespace Application.Handlers.Products
{ 
    public class DeleteProductCommandHandler(
    IGenericRepositories<Product> repo,
    IStringLocalizer localizer
    ) : IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        private readonly IGenericRepositories<Product> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<bool>> Handle(
            DeleteProductCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var product = await repo.GetByGuidIdAsync( request.Id,  cancellationToken );

                if (product is null)
                    return Result<bool>.Failure(localizer["ProductNotFound"],404);

                await repo.DeleteAsync(product);
                return Result<bool>.Success( true, localizer["Operationcompletedsuccessfully"]);
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(
                    $"{localizer["FailedToDeleteProduct"]}: {ex.Message}",
                    500
                );
            }
        }

}

}
