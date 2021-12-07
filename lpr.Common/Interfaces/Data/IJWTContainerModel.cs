using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces.Data
{
    public interface IJWTContainerModel
    {
        string SecurityAlogrithm { get; set; }
        int ExpireMinutes { get; set; }
        Claim[]? Claims { get; set; }
    }
}
