namespace DigitalBroker.Application.Exception
{
    public class NoPostFoundException(string message) : IOException(message);
}
