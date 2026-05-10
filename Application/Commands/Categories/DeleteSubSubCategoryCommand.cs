using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Categories
{
    public record DeleteSubSubCategoryCommand(Guid Id)
    : IRequest<Result<bool>>;

}
