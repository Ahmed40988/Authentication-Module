using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Categories
{
    public record CategoryUpdateRequestDTo
    (
        LocalizedDto ?Name,
        LocalizedDto ?Description
    );
}
