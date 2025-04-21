using DigitalBroker.Application.DTOs;
using MediatR;

namespace DigitalBroker.Application.Commands
{
    public class ForgetPasswordCommand : IRequest<bool>
    {
        public ForgetPassword ForgetPassword { get; set; }
    }
}
