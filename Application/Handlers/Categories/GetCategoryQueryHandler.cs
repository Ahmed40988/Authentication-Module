using Application.DTO.Localizes;
using Domain.Entities.Categories;
using global::Application.DTO.Categories;
using global::Application.Queries.Categories;
namespace Application.Handlers.Categories
{
        public class GetCategoryQueryHandler(
            IGenericRepositories<Category> repo,
            IStringLocalizer localizer
        ) : IRequestHandler<GetCategoryQuery,
            Result<CategoryResponseDetailsDto>>
        {
            private readonly IGenericRepositories<Category> repo = repo;
            private readonly IStringLocalizer localizer = localizer;

            public async Task<Result<CategoryResponseDetailsDto>> Handle(
                GetCategoryQuery request,
                CancellationToken cancellationToken)
            {
                var category = await repo.Query()
                    .Include(x => x.SubCategories)
                    .FirstOrDefaultAsync(
                        x => x.Id == request.Id,
                        cancellationToken
                    );

                if (category is null)
                {
                    return Result<CategoryResponseDetailsDto>.Failure(
                        localizer["CategoryNotFound"],
                        404
                    );
                }

                var dto = new CategoryResponseDetailsDto
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
                    category.IsActive,
                    category.SubCategories.Count
                );

                return Result<CategoryResponseDetailsDto>.Success(
                    dto,
                    localizer["Operationcompletedsuccessfully"]
                );
            }
        }
    }
