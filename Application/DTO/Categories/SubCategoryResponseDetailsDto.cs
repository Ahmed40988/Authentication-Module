using Application.DTO.Localizes;

namespace Application.DTO.Categories
{
        public record SubCategoryResponseDetailsDto
        (
            Guid Id,
            LocalizedDto Name,
            LocalizedDto? Description,
            Guid CategoryId,
            string CategoryName,
            bool IsActive               
        );
}