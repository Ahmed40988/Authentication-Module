using Application.DTO.Localizes;

namespace Application.DTO.Products
{

    public record ProductResponseDto
    (
        Guid Id,
        LocalizedDto? Name,
        LocalizedDto? Description,
        string? SKU,
        decimal Price,
        string? ImageUrl,
        int StockQuantity,
        string? Category,
        bool IsActive,
        DateTime CreatedAt,
        DateTime? UpdatedAt
    );

}