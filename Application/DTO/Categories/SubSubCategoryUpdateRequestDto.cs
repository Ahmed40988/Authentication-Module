using Application.DTO.Localizes;

namespace Application.DTO.Categories
{
    public record SubSubCategoryUpdateRequestDto
    (
        Guid? SubCategoryId,
        LocalizedDto? Name,
        LocalizedDto? Description
    );
}