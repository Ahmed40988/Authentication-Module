using Application.DTO.Brands;
using Application.DTOs.Brands;

namespace Application.Commands.Brands
{
    public  record CreateBrandCommand(BrandRequestDto Dto ):IRequest<Result<Guid>>;
}

