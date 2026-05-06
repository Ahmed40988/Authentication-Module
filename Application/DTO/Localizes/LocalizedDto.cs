using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Localizes
{
    public class LocalizedDto
    {
        public string? EN { get; set; }
        public string? AR { get; set; }
    }
    public class LocalizedNameDto
    {
        public Guid? Id { get; set; }
        public string? EN { get; set; }
        public string? AR { get; set; }
    }

}

