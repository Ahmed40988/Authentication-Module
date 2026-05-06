using Application.DTO.Localizes;

namespace Application.DTOs.Brands
{
    public sealed record UpdateBrandDto(
        LocalizedDto Name,
        LocalizedDto? Description,
        IFormFile? LogoUrl,
        bool ?IsActive
    );
}