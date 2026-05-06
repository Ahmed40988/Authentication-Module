namespace Application.Commands.Brands
{
    public  record DeleteBrandCommand(Guid Id) : IRequest<Result<bool>>;
}