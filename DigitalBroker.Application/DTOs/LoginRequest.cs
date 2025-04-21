
using DigitalBroker.Application.DTOs.Common;

namespace DigitalBroker.Application.DTOs
{
    public class LoginRequest : AccountBaseDto
    {
        public required string Password { get; init; }
    }
}
