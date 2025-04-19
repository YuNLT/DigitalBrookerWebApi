using DigitalBrooker.Domain.Entities.Models;
namespace DigitalBroker.Application.Abstracts
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task UpdateAsync(User user);
        Task<PasswordReset?> GetResetTokenByUserAsync(User user);
        Task UpdateTokenAsync(PasswordReset passwordReset);
        Task<User?> GetByIdAsync(Guid id);
        Task<string> GetNameById(string id);
        Task<User?> GetByRefreshTokenAsync(string token);
        Task<User?> GetByEmailWithRefreshTokenAsync(string email);
        Task<User?> GetByEmailWithRefreshTokenAsync(string email, string refreshToken);
        Task<PasswordReset?> GetValidPasswordResetTokenAsync(Guid token);
        Task UpdateUserPasswordAsync(User user);
        Task DeletePasswordResetTokenAsync(PasswordReset token);
    }
}
