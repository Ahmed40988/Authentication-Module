using Application.DTO.Products;
using Application.Queries.Products;
using Domain.Entities.Products;

namespace Application.Handlers.Products
{
    public class GetProductQueryHandler(
        IGenericRepositories<Product> repo,
        IStringLocalizer localizer
    ) : IRequestHandler<
        GetProductQuery,
        Result<ProductResponseDetailsDto>>
    {
        private readonly IGenericRepositories<Product> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<ProductResponseDetailsDto>> Handle(
            GetProductQuery request,
            CancellationToken cancellationToken)
        {
            var product = await repo.Query()
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.SubCategory)
                .Include(x => x.SubSubCategory)
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken
                );

            if (product is null)
            {
                return Result<ProductResponseDetailsDto>.Failure(
                    localizer["ProductNotFound"],
                    404
                );
            }

            var dto = new ProductResponseDetailsDto
            (
                product.Id,

                new LocalizedDto
                {
                    EN = product.Name.En,
                    AR = product.Name.Ar
                },

                product.Description != null
                    ? new LocalizedDto
                    {
                        EN = product.Description.En,
                        AR = product.Description.Ar
                    }
                    : null,

                product.BrandId,
                product.Brand.Name.En,

                product.CategoryId,
                product.Category.Name.En,

                product.SubCategoryId,
                product.SubCategory != null
                    ? product.SubCategory.Name.En
                    : null,

                product.SubSubCategoryId,
                product.SubSubCategory != null
                    ? product.SubSubCategory.Name.En
                    : null,

                product.SKU,
                product.Price,
                product.ImageUrl,
                product.StockQuantity,
                product.IsActive
            );

            return Result<ProductResponseDetailsDto>.Success(
                dto,
                localizer["Operationcompletedsuccessfully"]
            );
        }
    }
}