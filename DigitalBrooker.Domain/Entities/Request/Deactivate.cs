using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.Entities.Request
{
    public class Deactivate
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
