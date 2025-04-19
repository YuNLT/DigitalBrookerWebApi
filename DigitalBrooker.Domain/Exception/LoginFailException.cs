namespace DigitalBrooker.Domain.Exception
{
    public class LoginFailException(string email) : IOException($"Login failed for user with email {email} or password");
}
