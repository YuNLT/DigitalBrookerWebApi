using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Abstracts
{
    public interface ISmtpEmailService
    {
        Task SentPasswordAsync(string toEmail, string subject, string body);
    }
}
