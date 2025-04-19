namespace DigitalBrooker.Domain.Exception
{
    public class ForgetPasswordFailException(string email):IOException($"No user associated with {email}");
}
