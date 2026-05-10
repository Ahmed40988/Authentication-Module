
using Application.Commands.Products;
using Domain.Entities.Products;

namespace Application.Handlers.Products
{ 
    public class ToggleProductStatusCommandHandler(
    IGenericRepositories<Product> repo,
    IStringLocalizer localizer
    ) : IRequestHandler<ToggleProductStatusCommand, Result<bool>>
    {
    private readonly IGenericRepositories<Product> repo = repo;
    private readonly IStringLocalizer localizer = localizer;


    public async Task<Result<bool>> Handle(
        ToggleProductStatusCommand request,
        CancellationToken cancellationToken)
    {
        var product = await repo.GetByGuidIdAsync(request.Id, cancellationToken  );

        if (product is null)
            return Result<bool>.Failure(localizer["ProductNotFound"]);

        product.ToggleStatus();
        await repo.UpdateAsync(product,cancellationToken);

        return Result<bool>.Success(true, localizer["Operationcompletedsuccessfully"]);
    }
}
}

