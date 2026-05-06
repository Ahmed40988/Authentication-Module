using Application.Commands.Brands;
using Application.DTO.Brands;
using Application.DTO.Localizes;
using Application.DTO.Products;
using Application.Queries.Brands;
using Domain.Entities.Cataloges;

namespace Application.Handlers.Brands
{
    public class GetBrandQueryHandler(
        IGenericRepositories<Brand> repo,
        IStringLocalizer localizer
    ) : IRequestHandler<GetBrandQuery, Result<BrandDetailsDto>>
    {
        private readonly IGenericRepositories<Brand> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<BrandDetailsDto>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
        {
            var brand = await repo.Query()
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (brand is null)
                return Result<BrandDetailsDto>.Failure(localizer["BrandNotFound"]);

            var productsDto = brand.Products?
                .Select(p => new ProductResponseDto
                (
                    Id: p.Id,

                    Name: new LocalizedDto
                    {
                        EN = p.Name.En,
                        AR = p.Name.Ar
                    },

                    Description: p.Description is not null
                        ? new LocalizedDto
                        {
                            EN = p.Description.En,
                            AR = p.Description.Ar
                        }
                        : null,
                        p.SKU,
                        p.Price,
                        p.imageUrl,
                        p.StockQuantity,
                        p.Category?.Name?.En,
                        p.IsActive,
                        p.CreatedAt,
                        p.UpdatedAt
                        ))
                        .ToList();

            var brandDto = new BrandDetailsDto(
                new LocalizedDto
                {
                    EN = brand.Name.En,
                    AR = brand.Name.Ar
                },
                brand.Description is not null
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

            return Result<BrandDetailsDto>.Success(brandDto, localizer["Operationcompletedsuccessfully"]);
        }
    }
}