namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
       
        //DbSet<ApplicationUser> ApplicationUsers { get; }
        //DbSet<IdentityRole> Roles { get; }
        //DbSet<IdentityUserRole<string>> UserRoles { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}