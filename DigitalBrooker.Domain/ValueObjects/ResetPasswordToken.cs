using DigitalBrooker.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.ValueObjects
{
    public record ResetPasswordToken
    {
        public Guid Value { get;}
        public ResetPasswordToken(Guid value) 
        {
            if (value == Guid.Empty)
            {
                throw new EmptyResetPasswordTokenException("Reset Password Token cannot be empty");
            }
            Value = value;
        }

        public static implicit operator Guid(ResetPasswordToken token) => token.Value;
        public static implicit operator ResetPasswordToken(string resettoken) => new(resettoken);
    }
}
