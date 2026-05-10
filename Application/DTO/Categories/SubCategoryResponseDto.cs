using Application.DTO.Localizes;

namespace Application.DTO.Categories
{
    public record SubCategoryResponseDto
    (
        Guid Id,
        Guid CategoryId,
        LocalizedDto Name,
        LocalizedDto? Description,
        bool IsActive
    );
}