namespace DigitalBrooker.Domain.Exception
{
    public class UserAlreadyExistException(string email) : IOException($"User with email {email} already exists.");
}
