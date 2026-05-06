using Application.DTO.Localizes;

namespace Application.DTO.Brands
{
    public record BrandsResponseDto(
        Guid Id,
        LocalizedDto Name,
        LocalizedDto Description,
        string? LogoUrl,
        bool IsActive,
        int TotlalProducts
    );
}