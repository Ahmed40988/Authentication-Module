using Application.DTO.Localizes;
using Application.DTO.Products;

namespace Application.DTO.Brands
{
    public record BrandDetailsDto(
                LocalizedDto Name,
                LocalizedDto? Description,
                string? LogoUrl,
                bool IsActive,
                List<ProductResponseDto>? Products
            );

}