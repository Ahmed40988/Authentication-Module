using Application.DTOs.Brands;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public record UpdateBrandCommand(Guid Id, UpdateBrandDto Dto) : IRequest<Result<bool>>;
}