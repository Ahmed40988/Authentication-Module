using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Auth;
using Application.Interfaces.Auth;
using Domain.Entities.AuthModules;
using Domain.Entities.RoleModule;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.AuthModules
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<string>> GetAllPermissionNamesAsync()
        {
            return await _dbContext.Set<Permission>()
                .Select(p => p.Permission_Name)
                .ToListAsync();
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Set<User>()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _dbContext.Set<User>()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByEmailOrPhoneAsync(string target)
        {
            return await _dbContext.Set<User>()
                .FirstOrDefaultAsync(u =>
                    u.Email == target ||
                    u.PhoneNumber == target);
        }

        public async Task<RoleDTO?> GetRoleByNameAsync(string roleName)
        {
            var role = await _dbContext.Roles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);

            if (role == null) return null;

            return new RoleDTO
            {
                Id = role.RolePermission_Id.ToString(),
                Name = role.RoleName!
            };
        }

        public async Task<List<PermissionDTO>> GetPermissionsByRoleIdAsync(string roleId)
        {
            return await _dbContext.Set<RolePermission>()
                .Where(rp => rp.RolePermission_Id.ToString() == roleId)
                .Include(rp => rp.Permission)
                .Select(rp => new PermissionDTO
                {
                    Id = rp.Permission.Permission_Id,
                    Name = rp.Permission.Permission_Name
                })
                .ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Set<User>().Update(user);
            await _dbContext.SaveChangesAsync();
        }

       
    }
}

