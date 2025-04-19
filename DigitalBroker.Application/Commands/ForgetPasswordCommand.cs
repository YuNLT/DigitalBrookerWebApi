using MediatR;

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
