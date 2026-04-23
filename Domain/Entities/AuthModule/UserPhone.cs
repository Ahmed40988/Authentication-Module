using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awael_Al_Joudah.Domain.Entities.AuthModules;

public class UserPhone
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public string PhoneNumber { get; set; }

    public bool IncludeWhatsApp { get; set; }
}
