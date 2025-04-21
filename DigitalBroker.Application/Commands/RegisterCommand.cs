using DigitalBroker.Application.DTOs;
using MediatR;
namespace DigitalBroker.Application.Commands
{
    public class RegisterCommand : IRequest<Unit>
    {
       public RegisterRequest registerRequest { get; set; }
    }
}
