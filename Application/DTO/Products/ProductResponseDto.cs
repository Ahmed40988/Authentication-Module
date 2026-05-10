
namespace Application.DTO.Products;


public record ProductResponseDto
(
    Guid Id,
    LocalizedDto Name,
    LocalizedDto? Description,
    string SKU,
    decimal Price,
    string? ImageUrl,
    int StockQuantity,
    bool IsActive,
     Guid BrandId,
    Guid CategoryId,
    Guid? SubCategoryId,
    Guid? SubSubCategoryId
);
