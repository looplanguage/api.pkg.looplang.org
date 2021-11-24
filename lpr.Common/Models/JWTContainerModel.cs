using lpr.Common.Interfaces.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models
{
    public class JWTContainerModel : IJWTContainerModel
    {
        public string SecurityAlogrithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; } = 10080;
        public Claim[]? Claims { get; set; }
    }
}
