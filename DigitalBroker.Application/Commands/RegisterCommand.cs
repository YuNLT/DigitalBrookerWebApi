using DigitalBroker.Application.DTOs;
using MediatR;
namespace DigitalBroker.Application.Commands
{
    public class RegisterCommand : IRequest<bool>
    {
       public RegisterRequest registerRequest { get; set; }
    }
}
