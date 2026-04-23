using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.AuthModules
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }= string.Empty;

        public ICollection<UserPhone> UserPhones { get; set; } = new HashSet<UserPhone>();
        public ICollection<UserAddress> UserAddresses { get; set; } = new HashSet<UserAddress>();


    }
}
