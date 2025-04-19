namespace DigitalBrooker.Domain.Exception
{
    public class RegistrationfailException(IEnumerable<string> error) : IOException($"Registration failed with error: {string.Join(Environment.NewLine, error)}");
}
   
