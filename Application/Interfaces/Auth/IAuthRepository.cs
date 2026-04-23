using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Auth;
using Domain.Entities.AuthModules;

namespace Application.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> GetUserByEmailOrPhoneAsync(string target);
        Task<RoleDTO?> GetRoleByNameAsync(string roleName);
        Task<List<PermissionDTO>> GetPermissionsByRoleIdAsync(string roleId);
        Task<List<string>> GetAllPermissionNamesAsync();
        Task UpdateUserAsync(User user);
    }
}
