using Application.Commands.Categories;
using Domain.Entities.Products;
using Domain.Entities.SubSubCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Categories
{
    public class DeleteSubSubCategoryCommandHandler(
    IGenericRepositories<SubSubCategory> repo,
    IGenericRepositories<Product> Productrepo,
    IStringLocalizer localizer
    ) : IRequestHandler<DeleteSubSubCategoryCommand, Result<bool>>
    {
        private readonly IGenericRepositories<SubSubCategory> repo = repo;
        private readonly IGenericRepositories<Product> productrepo = Productrepo;
        private readonly IStringLocalizer localizer = localizer;


        public async Task<Result<bool>> Handle(
            DeleteSubSubCategoryCommand request,
            CancellationToken cancellationToken)
                {
            try
            {
                var subSubCategory = await repo.GetByGuidIdAsync(
                    request.Id,
                    cancellationToken
                );

                if (subSubCategory is null)
                {
                    return Result<bool>.Failure(
                        localizer["SubSubCategoryNotFound"],
                        404
                    );
                }
                var hasProducts = await productrepo.AnyAsync(
             x => x.SubSubCategoryId == request.Id);

                if (hasProducts)
                {
                    return Result<bool>.Failure(
                        localizer["CannotDeleteLinkedEntity"],
                        4001
                    );
                }

                await repo.DeleteAsync(subSubCategory);

                return Result<bool>.Success(
                    true,
                    localizer["Operationcompletedsuccessfully"]
                );
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(
                    $"{localizer["FailedToDeleteSubSubCategory"]}: {ex.Message}",
                    500
                );
            }
        }

}

}
