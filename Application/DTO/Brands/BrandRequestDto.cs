using Application.DTO.Localizes;

namespace Application.DTO.Brands
{
  
        public  record BrandRequestDto(
           LocalizedDto Name,
           LocalizedDto? Description,
            IFormFile? LogoUrl,
            bool IsActive,
            List<Guid>? CategoryIds
        );
    
}
