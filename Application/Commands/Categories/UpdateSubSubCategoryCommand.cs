using Application.DTO.Categories;

namespace Application.Commands.Categories
{
    public record UpdateSubSubCategoryCommand(Guid Id, SubSubCategoryUpdateRequestDto? Dto) : IRequest<Result<bool>>;
}