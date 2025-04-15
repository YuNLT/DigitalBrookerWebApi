using DigitalBrooker.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Abstracts
{
    public interface IAuthTokenProcessor
    {
        (string token, DateTime expireTime) GenerateToken(User user, IList<string> roles);
        string GenerateRefreshToken();
        void WriteTokenInHttpOnlyCookie(string cookieName, string token, DateTime expireTime);
    }
}
