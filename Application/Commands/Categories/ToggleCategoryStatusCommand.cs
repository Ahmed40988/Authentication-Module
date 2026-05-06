using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Categories
{
        public record ToggleCategoryStatusCommand(Guid Id)
            : IRequest<Result<bool>>;
    
}
