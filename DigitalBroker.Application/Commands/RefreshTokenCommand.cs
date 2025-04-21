using MediatR;

namespace DigitalBroker.Application.Commands
{
    public class RefreshTokenCommand: IRequest<Unit>
    {
        public string? RefreshToken { get; set; }
        public RefreshTokenCommand(string? refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
