using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.DTOs
{
    public record ResetPassword
    {
        public required Guid ResetPasswordToken { get; set; }
        public required string Password { get; set; }
    }
}
