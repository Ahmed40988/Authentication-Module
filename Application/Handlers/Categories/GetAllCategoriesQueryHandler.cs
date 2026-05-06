using Application.Common.Abstractions;
using Application.DTO.Localizes;
using Domain.Entities.Categories;
using global::Application.DTO.Categories;
using global::Application.Queries.Categories;
namespace Application.Handlers.Categories
{
        public class GetAllCategoriesQueryHandler(
            IGenericRepositories<Category> repo,
            IStringLocalizer localizer
        ) : IRequestHandler<GetAllCategoriesQuery,
            Result<PaginatedList<CategoryResponseDto>>>
        {
            private readonly IGenericRepositories<Category> repo = repo;
            private readonly IStringLocalizer localizer = localizer;

            public async Task<Result<PaginatedList<CategoryResponseDto>>> Handle(
                GetAllCategoriesQuery request,
                CancellationToken cancellationToken)
            {
                var query = repo.Query()
                    .Select(category => new CategoryResponseDto
                    (
                        category.Id,
                        new LocalizedDto
                        {
                            EN = category.Name.En,
                            AR = category.Name.Ar
                        },

                        category.Description != null
                            ? new LocalizedDto
                            {
                                EN = category.Description.En,
                                AR = category.Description.Ar
                            }
                            : null,
                        category.IsActive
                    ));

                var paginatedCategories =
                    await PaginatedList<CategoryResponseDto>
                    .CreateAsync(
                        query,
                        request.PageNumber,
                        request.PageSize
                    );

                return Result<PaginatedList<CategoryResponseDto>>
                    .Success(
                        paginatedCategories,
                        localizer["Operationcompletedsuccessfully"]
                    );
            }
        }
    }
