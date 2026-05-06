using Application.DTO.Brands;

namespace Application.Queries.Brands
{
    public record GetBrandQuery(Guid Id) : IRequest<Result<BrandDetailsDto>>;
}