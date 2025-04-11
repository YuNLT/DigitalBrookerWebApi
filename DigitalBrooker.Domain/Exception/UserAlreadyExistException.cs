using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.Exception
{
    public class UserAlreadyExistException(string email) : IOException($"User with email {email} already exists.");
}
