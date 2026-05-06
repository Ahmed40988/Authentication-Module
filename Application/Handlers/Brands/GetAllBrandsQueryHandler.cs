using Application.Common.Abstractions;
using Application.DTO.Brands;
using Application.DTO.Localizes;
using Application.Queries.Brands;
using Domain.Entities.Cataloges;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Brands
{
    public class GetAllBrandsQueryHandler(
        IGenericRepositories<Brand> repo,
        IStringLocalizer localizer
    ) : IRequestHandler<GetAllBrandsQuery, Result<PaginatedList<BrandsResponseDto>>>
    {
        private readonly IGenericRepositories<Brand> repo = repo;
        private readonly IStringLocalizer localizer = localizer;

        public async Task<Result<PaginatedList<BrandsResponseDto>>> Handle(
            GetAllBrandsQuery request,
            CancellationToken cancellationToken)
        {
            var query = repo.Query()
                .Include(x => x.Products)
                .Select(brand => new BrandsResponseDto
                (
                    brand.Id,

                    new LocalizedDto
                    {
                        EN = brand.Name.En,
                        AR = brand.Name.Ar
                    },

                    new LocalizedDto
                    {
                        EN = brand.Description != null ? brand.Description.En : null,
                        AR = brand.Description != null ? brand.Description.Ar : null
                    },

                    brand.LogoUrl,

                    brand.IsActive,

                    brand.Products != null
                        ? brand.Products.Count
                        : 0
                ));

            var paginatedBrands = await PaginatedList<BrandsResponseDto>
                .CreateAsync(
                    query,
                    request.PageNumber,
                    request.PageSize
                );

            return Result<PaginatedList<BrandsResponseDto>>
                .Success(
                    paginatedBrands,
                    localizer["Operationcompletedsuccessfully"]
                );
        }
    }
}