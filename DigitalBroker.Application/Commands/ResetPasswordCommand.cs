using DigitalBroker.Application.DTOs;
using MediatR;
namespace DigitalBroker.Application.Commands
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public ResetPassword ResetPassword { get; set; }
    }
}
