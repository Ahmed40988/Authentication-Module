using Application.DTO.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Categories
{
        public record GetCategoryQuery(Guid Id)
            : IRequest<Result<CategoryResponseDetailsDto>>;
    }
