using DigitalBroker.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.DTOs
{
    public class RegisterRequest : AccountBaseDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; init; }
    }
}
