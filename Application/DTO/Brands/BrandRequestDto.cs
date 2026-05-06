using Application.DTO.Localizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Brands
{
  
        public  record BrandRequestDto(
            Guid Id,
           LocalizedDto Name,
           LocalizedDto? Description,
            IFormFile? LogoUrl,
            bool IsActive
        );
    
}
