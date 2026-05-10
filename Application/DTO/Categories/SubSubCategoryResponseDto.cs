using Application.DTO.Localizes;

namespace Application.DTO.Categories
{
    public record SubSubCategoryResponseDto
    (
        Guid Id,
        Guid SubCategoryId,
        LocalizedDto Name,
        LocalizedDto? Description,
        bool IsActive
    );
}