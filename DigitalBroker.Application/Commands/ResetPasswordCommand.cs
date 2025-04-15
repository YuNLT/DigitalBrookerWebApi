using DigitalBrooker.Domain.Entities.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Commands
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public Guid ResetPasswordToken { get; set; }
        public string Password { get; set; }
        public ResetPasswordCommand(Guid resetPasswordToken, string password)
        {
            ResetPasswordToken = resetPasswordToken;
            Password = password;
        }
    }
}
