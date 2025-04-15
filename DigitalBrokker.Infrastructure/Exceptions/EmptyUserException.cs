using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrokker.Infrastructure.Exceptions
{
    public class EmptyUserException(string email) : Exception($"User with {email} cannot be found");
}
