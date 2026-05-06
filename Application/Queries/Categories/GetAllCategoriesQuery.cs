using Application.Common.Abstractions;
using Application.DTO.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Categories
{
        public record GetAllCategoriesQuery(
            int PageNumber = 1,
            int PageSize = 10
        ) : IRequest<Result<PaginatedList<CategoryResponseDto>>>;
}
