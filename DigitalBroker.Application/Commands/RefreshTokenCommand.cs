using MediatR;

namespace DigitalBroker.Application.Commands
{
    public class RefreshTokenCommand: IRequest<bool>
    {
        public string? RefreshToken { get; set; }
        public RefreshTokenCommand(string? refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
