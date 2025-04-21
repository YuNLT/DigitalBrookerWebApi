
namespace DigitalBroker.Application.Exception
{
    public class IsActiveException(string email) : IOException($"User with {email}  is not active");
}
