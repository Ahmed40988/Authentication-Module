namespace Application.DTO.Categories
{
        public record DeleteCategoryCommand(Guid Id)
            : IRequest<Result<bool>>;
    
}
