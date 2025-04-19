using DigitalBrooker.Domain.Entities.Request;
using MediatR;
namespace DigitalBroker.Application.Commands
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public Guid ResetPasswordToken { get; set; }
        public string Password { get; set; }
        public ResetPasswordCommand(Guid resetPasswordToken, string password)
        {
            ResetPasswordToken = resetPasswordToken;
            Password = password;
        }
    }
}
