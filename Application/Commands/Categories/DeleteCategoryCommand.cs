namespace Application.Commands.Categories
{
        public record DeleteCategoryCommand(Guid Id)
            : IRequest<Result<bool>>;
    
}
