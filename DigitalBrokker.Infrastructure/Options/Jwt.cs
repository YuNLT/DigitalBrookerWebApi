using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrokker.Infrastructure.Options
{
    public class Jwt
    {
        public const string jwtkey = "Jwt"; //to fetch the jwt options from the appsettings.json
        public string? Secret{ get; set; } //key to encrypt the token
        public string? Issuer { get; set; } //who is issuing the token
        public string? Audience { get; set; } //who is the audience of the token
        public int ExpireTime { get; set; }

    }
}
