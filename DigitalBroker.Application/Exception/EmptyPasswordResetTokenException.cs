
namespace DigitalBroker.Application.Exception
{
    public class EmptyPasswordResetTokenException(string message) : IOException(message);
}
