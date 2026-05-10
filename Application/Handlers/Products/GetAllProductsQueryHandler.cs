using Application.DTO.Products;
using Application.Queries.Products;
using Domain.Entities.Products;

namespace Application.Handlers.Products
{
    public class GetAllProductsQueryHandler(
        IGenericRepositories<Product> repo,
        IStringLocalizer localizer
    ) : IRequestHandler<
        GetAllProductsQuery,
        Result<PaginatedList<ProductResponseDto>>>
    {
        private readonly IGenericRepositories<Product> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<PaginatedList<ProductResponseDto>>> Handle(
            GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var query = repo.Query()
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.SubCategory)
                .Include(x => x.SubSubCategory)
                .AsQueryable();

            if (request.BrandId.HasValue)
                query = query.Where(x =>
                    x.BrandId == request.BrandId.Value);

            if (request.CategoryId.HasValue)
                query = query.Where(x =>
                    x.CategoryId == request.CategoryId.Value);

            var productsQuery = query
                .Select(product => new ProductResponseDto
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
                    product.SKU,
                    product.Price,
                    product.ImageUrl,
                    product.StockQuantity,
                    product.IsActive,
                    product.BrandId,
                    product.CategoryId,
                    product.SubCategoryId,
                    product.SubSubCategoryId
                ));

            var paginatedProducts =
                await PaginatedList<ProductResponseDto>
                .CreateAsync(
                    productsQuery,
                    request.PageNumber,
                    request.PageSize
                );

            return Result<PaginatedList<ProductResponseDto>>
                .Success(
                    paginatedProducts,
                    localizer["Operationcompletedsuccessfully"]
                );
        }
    }
}