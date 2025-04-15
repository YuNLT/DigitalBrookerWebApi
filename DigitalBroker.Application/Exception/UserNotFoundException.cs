using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Exception
{
    public class UserNotFoundException(string message):IOException(message);
}
