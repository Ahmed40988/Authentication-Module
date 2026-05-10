using Application.DTO.Localizes;

namespace Application.DTO.Categories
{
    public record SubSubCategoryRequestDto
    (
        Guid SubCategoryId,
        LocalizedDto Name,
        LocalizedDto? Description
    );
}