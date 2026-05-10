using Application.DTO.Brands;
using Application.DTO.Localizes;
using Application.DTO.Products;
using Application.Queries.Brands;
using Domain.Entities.Cataloges;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Handlers.Brands
{
    public class GetBrandQueryHandler(
        IGenericRepositories<Brand> repo,
        IStringLocalizer localizer
    ) : IRequestHandler<GetBrandQuery, Result<BrandDetailsDto>>
    {
        private readonly IGenericRepositories<Brand> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<BrandDetailsDto>> Handle(
            GetBrandQuery request,
            CancellationToken cancellationToken)
        {
            var brand = await repo.Query()
                .Include(x => x.Products)
                    .ThenInclude(x => x.Category)
                    .ThenInclude(x => x.SubCategories)
                    .ThenInclude(x=>x.SubSubCategories)
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken
                );

            if (brand is null)
            {
                return Result<BrandDetailsDto>.Failure(
                    localizer["BrandNotFound"],
                    404
                );
            }

            var productsDto = brand.Products
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
                ))
                .ToList();

            var dto = new BrandDetailsDto
            (
                brand.Id,
                new LocalizedDto
                {
                    EN = brand.Name.En,
                    AR = brand.Name.Ar
                },
                brand.Description != null
                    ? new LocalizedDto
                    {
                        EN = brand.Description.En,
                        AR = brand.Description.Ar
                    }
                    : null,
                brand.LogoUrl,
                brand.IsActive,
                productsDto
            );

            return Result<BrandDetailsDto>.Success(
                dto,
                localizer["Operationcompletedsuccessfully"]
            );
        }
    }
}