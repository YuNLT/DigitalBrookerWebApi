using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Commands
{
    public class SeedRoleToSellerCommand : IRequest<string>
    {
        public string Email { get; set; }
        public SeedRoleToSellerCommand(string email)
        {
            Email = email;
        }
    }
}
