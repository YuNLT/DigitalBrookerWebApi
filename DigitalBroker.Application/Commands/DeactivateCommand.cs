using DigitalBroker.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Commands
{
    public class DeactivateCommand : IRequest<Unit>
    { 
        public Deactivate Deactivate { get; set; }
    }
}
