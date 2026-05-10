using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Categories
{
    public record SubCategoryUpdateRequestDto
        (
        Guid ?CategoryId,
        LocalizedDto? Name,
        LocalizedDto? Description
    );
}
