using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.Entities.Request
{
    public class ResetPassword
    {
        public required Guid ResetPasswordToken { get; set; }
        public required string Password { get; set; }
    }
}
