namespace Application.DTO.Products;
public record ProductUpdateRequestDto(
    LocalizedDto Name,
    LocalizedDto? Description,
    decimal Price,
    int StockQuantity,
    Guid? BrandId,
    Guid? CategoryId,
    Guid? SubCategoryId,
    Guid? SubSubCategoryId,
    IFormFile? ImageUrl
);






