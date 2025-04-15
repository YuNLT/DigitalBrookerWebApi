using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrokker.Infrastructure.Exceptions
{
    public class PasswordUpdateError(string message):Exception(message);
}
