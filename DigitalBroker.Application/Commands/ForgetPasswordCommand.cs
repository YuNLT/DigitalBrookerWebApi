using DigitalBrooker.Domain.Entities.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Commands
{
    public class ForgetPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; } 
        public ForgetPasswordCommand(string email)
        {
            Email = email;
        }
    }
}
