using DigitalBroker.Application.DTOs;
using MediatR;

namespace DigitalBroker.Application.Commands
{
    public class LoginCommand : IRequest<Unit>
    {
        public LoginRequest LoginRequest { get; set; }
    }
}
