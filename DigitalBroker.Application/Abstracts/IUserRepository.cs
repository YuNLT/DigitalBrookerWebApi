using DigitalBrooker.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Abstracts
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task UpdateAsync(User user);
        Task<PasswordReset?> GetResetTokenByUserAsync(User user);
        Task UpdateTokenAsync(PasswordReset passwordReset);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByRefreshTokenAsync(string token);
        Task<User?> GetByEmailWithRefreshTokenAsync(string email);
        Task<User?> GetByEmailWithRefreshTokenAsync(string email, string refreshToken);
        Task<PasswordReset?> GetValidPasswordResetTokenAsync(Guid token);
        Task UpdateUserPasswordAsync(User user);
        Task DeletePasswordResetTokenAsync(PasswordReset token);
    }
}
