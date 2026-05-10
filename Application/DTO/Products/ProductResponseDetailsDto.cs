using Application.DTO.Localizes;

namespace Application.DTO.Products;

public record ProductResponseDetailsDto
(
    Guid Id,
    LocalizedDto Name,
    LocalizedDto? Description,
    Guid BrandId,
    string BrandName,
    Guid CategoryId,
    string CategoryName,
    Guid? SubCategoryId,
    string? SubCategoryName,
    Guid? SubSubCategoryId,
    string? SubSubCategoryName,
    string SKU,
    decimal Price,
    string? ImageUrl,
   int StockQuantity,
    bool IsActive
);