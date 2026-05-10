using Application.DTO.Localizes;

namespace Application.DTO.Categories
{
    public record SubCategoryRequestDto
    (
        Guid CategoryId,
        LocalizedDto Name,
        LocalizedDto? Description
    );
}