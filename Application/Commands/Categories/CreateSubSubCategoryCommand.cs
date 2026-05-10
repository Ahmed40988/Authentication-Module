using Application.DTO.Categories;

namespace Application.Commands.Categories
{
    public record CreateSubSubCategoryCommand(SubSubCategoryRequestDto Dto) : IRequest<Result<Guid>>;
}