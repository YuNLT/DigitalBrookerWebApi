namespace DigitalBroker.Application.Abstracts
{
    public interface ISmtpEmailService
    {
        Task SentPasswordAsync(string toEmail, string subject, string body);
    }
}
