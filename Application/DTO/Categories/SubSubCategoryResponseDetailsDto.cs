using Application.DTO.Localizes;

namespace Application.DTO.Categories
{
    public record SubSubCategoryResponseDetailsDto
    (
        Guid Id,
        LocalizedDto Name,
        LocalizedDto? Description,
        Guid SubCategoryId,
        string SubCategoryName,
        bool IsActive
    );
}