using Domain.Entities.AuthModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Base
{
    public abstract class BaseEntity : IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedById { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedById { get; set; }
    
        public bool IsActive { get; protected set; } = true;

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }


}

