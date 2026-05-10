
using Application.DTO.Localizes;

namespace Application.DTO.Products;

public record ProductRequestDto(
    Guid BrandId,
    Guid CategoryId,
    Guid? SubCategoryId,
    Guid? SubSubCategoryId,
    LocalizedDto Name,
    LocalizedDto? Description,
    string SKU,
    decimal Price,
    int StockQuantity,
    IFormFile? ImageUrl
);
