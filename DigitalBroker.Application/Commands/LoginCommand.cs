using MediatR;

namespace DigitalBroker.Application.Commands
{
    public class LoginCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
