using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.Exception
{
    public class RegistrationfailException(IEnumerable<string> error) : IOException($"Registration failed with error: {string.Join(Environment.NewLine, error)}");
}
   
