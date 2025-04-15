using DigitalBrooker.Domain.Constants;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using DigitalBroker.Application.Abstracts;

namespace DigitalBroker.Application.Services
{
    public class SmtpEmailService : ISmtpEmailService
    {
        public required SmtpInfo _smtpInfo;
        public SmtpEmailService(SmtpInfo smtpInfo)
        {
            _smtpInfo = smtpInfo;
        }
        public async Task SentPasswordAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_smtpInfo.smtpEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls); // Make sure this is StartTls
            await smtp.AuthenticateAsync(_smtpInfo.smtpEmail, _smtpInfo.smtpPassword); // Critical line
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
