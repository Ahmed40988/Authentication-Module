using Application.Commands.Products;
using Application.Common.Helpers;
using Domain.Entities.Cataloges;
using Domain.Entities.Categories;
using Domain.Entities.Products;
using Domain.Entities.SubCategories;
using Domain.Entities.SubSubCategories;

namespace Application.Handlers.Products;

public class UpdateProductCommandHandler(
IGenericRepositories<Product> repo,
IGenericRepositories<Brand> brandRepo,
IGenericRepositories<Category> categoryRepo,
IGenericRepositories<SubCategory> subCategoryRepo,
IGenericRepositories<SubSubCategory> subSubCategoryRepo,
IStringLocalizer localizer,
IFileStorageService fileStorage
) : IRequestHandler<UpdateProductCommand, Result<bool>>
{
    private readonly IGenericRepositories<Product> repo = repo;
    private readonly IGenericRepositories<Brand> brandRepo = brandRepo;
    private readonly IGenericRepositories<Category> categoryRepo = categoryRepo;
    private readonly IGenericRepositories<SubCategory> subCategoryRepo = subCategoryRepo;
    private readonly IGenericRepositories<SubSubCategory> subSubCategoryRepo = subSubCategoryRepo;
    private readonly IStringLocalizer localizer = localizer;
    private readonly IFileStorageService fileStorage = fileStorage;


public async Task<Result<bool>> Handle(
    UpdateProductCommand request,
    CancellationToken cancellationToken)
    {
        try
        {
            var dto = request.Dto;
            var product = await repo.GetByGuidIdAsync( request.Id,cancellationToken );

            if (product is null)
                return Result<bool>.Failure(localizer["ProductNotFound"], 404);

            var (nameAr, nameEn) = NormalizeHelper.Normalize(dto.Name.AR, dto.Name.EN);

            string? descAr = null;
            string? descEn = null;

            if (dto.Description is not null)
                (descAr, descEn) = NormalizeHelper.Normalize(dto.Description.AR,dto.Description.EN);

            var exists = await repo.AnyAsync(x =>
                x.Id != request.Id &&(x.Name.En == nameEn || x.Name.Ar == nameAr)  );

            if (exists)
                return Result<bool>.Failure( localizer["ProductAlreadyExists"]);  

            if (dto.BrandId.HasValue &&
                !await brandRepo.AnyAsync(x => x.Id == dto.BrandId.Value))
                return Result<bool>.Failure(
                    localizer["BrandNotFound"],
                    404
                );

            if (dto.CategoryId.HasValue &&
                !await categoryRepo.AnyAsync(x => x.Id == dto.CategoryId.Value))
                return Result<bool>.Failure(
                    localizer["CategoryNotFound"],
                    404
                );

            if (dto.SubCategoryId.HasValue &&
                !await subCategoryRepo.AnyAsync(x => x.Id == dto.SubCategoryId.Value))
                return Result<bool>.Failure(
                    localizer["SubCategoryNotFound"],
                    404
                );

            if (dto.SubSubCategoryId.HasValue &&
                !await subSubCategoryRepo.AnyAsync(x => x.Id == dto.SubSubCategoryId.Value))
                return Result<bool>.Failure(
                    localizer["SubSubCategoryNotFound"],
                    404
                );

            string? imagePath = product.ImageUrl;

            if (dto.ImageUrl is not null)
            {
                using var stream = dto.ImageUrl.OpenReadStream();
                imagePath = await fileStorage.SaveFileAsync(
                    stream,
                    dto.ImageUrl.FileName,
                    "Products/logos"
                );
            }

            product.Update(
                nameEn,
                nameAr,
                dto.Price,
                dto.StockQuantity,
                imagePath,
                descEn,
                descAr
            );

            await repo.UpdateAsync(product, cancellationToken);
            return Result<bool>.Success(true,localizer["Operationcompletedsuccessfully"]);
        }
        catch (Exception ex)
        {
            return Result<bool>.Error(
                $"{localizer["FailedToUpdateProduct"]}: {ex.Message}", 500);
        }
    }

}
