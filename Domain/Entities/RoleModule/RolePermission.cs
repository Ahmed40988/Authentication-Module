using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.RoleModule
{
    public class RolePermission
    {
        [Key]
        public Guid RolePermission_Id { get; set; }
        public string Role_Id { get; set; } 
        public Guid Permission_Id { get; set; }

        public IdentityRole Role { get; set; }
        public Permission Permission { get; set; }
    }
}
