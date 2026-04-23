using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RoleModule
{
    public class Permission
    {
        [Key]
        public Guid Permission_Id { get; set; }
        public string Permission_Name { get; set; }


        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
