using Domain.Entities.RoleModule;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.AuthModules
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }= string.Empty;
        public ICollection<RolePermission> Roles { get; set; } = new HashSet<RolePermission>();
    }
}
