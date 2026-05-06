using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBaseUrlService
    {
        string GetBaseUrl();
        string ToAbsoluteMediaUrl(string? path);

    }
}
