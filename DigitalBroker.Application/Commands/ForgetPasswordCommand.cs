using DigitalBroker.Application.DTOs;
using MediatR;

namespace DigitalBroker.Application.Commands
{
    public class ForgetPasswordCommand : IRequest<Unit>
    {
        public ForgetPassword ForgetPassword { get; set; }
    }
}
