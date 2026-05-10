using Application.Commands.Products;
using Application.Common.Helpers;
using Domain.Entities.Cataloges;
using Domain.Entities.Categories;
using Domain.Entities.Products;
using Domain.Entities.SubCategories;
using Domain.Entities.SubSubCategories;

namespace Application.Handlers.Products;

public class CreateProductCommandHandler(
    IGenericRepositories<Product> repo,
    IGenericRepositories<Brand> brandRepo,
    IGenericRepositories<Category> categoryRepo,
    IGenericRepositories<SubCategory> subCategoryRepo,
    IGenericRepositories<SubSubCategory> subSubCategoryRepo,
    IStringLocalizer localizer,
    IFileStorageService fileStorage
) : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    private readonly IGenericRepositories<Product> repo = repo;
    private readonly IGenericRepositories<Brand> brandRepo = brandRepo;
    private readonly IGenericRepositories<Category> categoryRepo = categoryRepo;
    private readonly IGenericRepositories<SubCategory> subCategoryRepo = subCategoryRepo;
    private readonly IGenericRepositories<SubSubCategory> subSubCategoryRepo = subSubCategoryRepo;
    private readonly IStringLocalizer localizer = localizer;
    private readonly IFileStorageService fileStorage = fileStorage;

    public async Task<Result<Guid>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var dto = request.Dto;

            var (nameAr, nameEn) = NormalizeHelper.Normalize(  dto.Name.AR, dto.Name.EN );
            string? descAr = null;
            string? descEn = null;
            if (dto.Description is not null)
                (descAr, descEn) = NormalizeHelper.Normalize(dto.Description.AR,  dto.Description.EN);


            if (await repo.AnyAsync(x => x.SKU == dto.SKU))
                return Result<Guid>.Failure(localizer["SkuAlreadyExists"], 400);

            if (!await brandRepo.AnyAsync(x => x.Id == dto.BrandId))
                return Result<Guid>.Failure(localizer["BrandNotFound"], 404);

            if (!await categoryRepo.AnyAsync(x => x.Id == dto.CategoryId))
                return Result<Guid>.Failure(localizer["CategoryNotFound"], 404);

            if (dto.SubCategoryId.HasValue &&
                !await subCategoryRepo.AnyAsync(x => x.Id == dto.SubCategoryId.Value))
                return Result<Guid>.Failure(localizer["SubCategoryNotFound"], 404);

            if (dto.SubSubCategoryId.HasValue &&
                !await subSubCategoryRepo.AnyAsync(x => x.Id == dto.SubSubCategoryId.Value))
                return Result<Guid>.Failure(localizer["SubSubCategoryNotFound"], 404);

            var exists = await repo.AnyAsync(x => x.Name.En == nameEn || x.Name.Ar == nameAr);
            if (exists)
                return Result<Guid>.Failure(localizer["ProductAlreadyExists"]);

            string? logoImagePath = null;
            if (dto.ImageUrl is not null)
            {
                using var stream = dto.ImageUrl.OpenReadStream();
                logoImagePath = await fileStorage.SaveFileAsync(
                    stream, dto.ImageUrl.FileName, "Products/logos");
            }

            var product = new Product(
                 nameEn,
                 nameAr,
                 descEn,
                 descAr,
                 dto.SKU,
                 dto.Price,
                 dto.StockQuantity,
                 logoImagePath,
                 dto.BrandId,
                 dto.CategoryId,
                 dto.SubCategoryId,
                 dto.SubSubCategoryId
                 );

            await repo.AddAsync(product);
            return Result<Guid>.Success(product.Id,  localizer["Operationcompletedsuccessfully"] );
        }
        catch (Exception ex)
        {
            return Result<Guid>.Error(
                $"{localizer["FailedToCreateProduct"]}: {ex.Message}",
                500
            );
        }
    }
}