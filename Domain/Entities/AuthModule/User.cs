
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awael_Al_Joudah.Domain.Entities.AuthModules
{
    public class User
    {
        public Guid ID { get; set; }
        public string FullName { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Password { get; set; }

        public ICollection<UserPhone> UserPhones { get; set; } = new HashSet<UserPhone>();
        public ICollection<UserAddress> UserAddresses { get; set; } = new HashSet<UserAddress>();


    }
}
