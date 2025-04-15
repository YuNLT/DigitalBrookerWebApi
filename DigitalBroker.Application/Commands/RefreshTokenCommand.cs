using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
