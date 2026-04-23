using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.AuthModules
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }= string.Empty;
    }
}
