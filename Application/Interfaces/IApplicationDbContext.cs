using Domain.Entities.AuthModule;
using Domain.Entities.AuthModules;
using Domain.Entities.RoleModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
       
        DbSet<User> ApplicationUsers { get; }
        DbSet<IdentityRole> Roles { get; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        DbSet<IdentityUserRole<string>> UserRoles { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}