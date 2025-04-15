using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.Exception
{
    public class ForgetPasswordFailException(string email):IOException($"No user associated with {email}");
}
