using Application.DTO.Localizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Categories
{
        public record CategoryResponseDto
        (
            Guid Id,
            LocalizedDto Name,
            LocalizedDto? Description,
            bool IsActive
        );
    }
