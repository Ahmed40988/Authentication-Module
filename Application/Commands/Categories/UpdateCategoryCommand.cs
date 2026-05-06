using Application.DTO.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Categories
{
        public record UpdateCategoryCommand(
            Guid Id,
            CategoryRequestDTo Dto
        ) : IRequest<Result<bool>>;
    }
