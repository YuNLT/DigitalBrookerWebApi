using DigitalBrooker.Domain.Entities.Models;
namespace DigitalBroker.Application.Abstracts
{
    public interface IAuthTokenProcessor
    {
        (string token, DateTime expireTime) GenerateToken(User user, IList<string> roles);
        string GenerateRefreshToken();
        void WriteTokenInHttpOnlyCookie(string cookieName, string token, DateTime expireTime);
    }
}
