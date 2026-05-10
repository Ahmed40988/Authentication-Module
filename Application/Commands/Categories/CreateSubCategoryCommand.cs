using Application.DTO.Categories;

namespace Application.Commands.Categories
{
    public record CreateSubCategoryCommand(SubCategoryRequestDto Dto): IRequest<Result<Guid>>;
}