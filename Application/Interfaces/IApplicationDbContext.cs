using Domain.Entities.AuthModule;
using Domain.Entities.AuthModules;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
       
        DbSet<User> ApplicationUsers { get; }
        DbSet<IdentityRole> Roles { get; }
        DbSet<IdentityUserRole<string>> UserRoles { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}