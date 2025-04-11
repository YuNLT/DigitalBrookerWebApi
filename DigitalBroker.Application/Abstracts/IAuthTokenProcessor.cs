using DigitalBrooker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Abstracts
{
    public interface IAuthTokenProcessor
    {
        (string token, DateTime expireTime) GenerateToken(User user);
        string GenerateRefreshToken();
        void WriteTokenInHttpOnlyCookie(string cookieName, string token, DateTime expireTime);
    }
}
