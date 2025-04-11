using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.Exception
{
    public class RefreshTokenException(string message) : IOException(message); 
}
