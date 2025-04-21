using DigitalBroker.Application.DTOs;
using MediatR;

namespace DigitalBroker.Application.Commands
{
    public class LoginCommand : IRequest<bool>
    {
        public LoginRequest LoginRequest { get; set; }
    }
}
