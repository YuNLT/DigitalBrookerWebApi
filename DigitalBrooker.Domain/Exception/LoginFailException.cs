using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.Exception
{
    public class LoginFailException(string email) : IOException($"Login failed for user with email {email} or password");
}
