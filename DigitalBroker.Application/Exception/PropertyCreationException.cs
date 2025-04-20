using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Exception
{
    public class PropertyCreationException(string message) : IOException(message);
}
