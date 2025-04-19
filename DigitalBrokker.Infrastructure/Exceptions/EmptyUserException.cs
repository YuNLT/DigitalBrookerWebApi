namespace DigitalBrokker.Infrastructure.Exceptions
{
    public class EmptyUserException(string email) : Exception($"User with {email} cannot be found");
}
