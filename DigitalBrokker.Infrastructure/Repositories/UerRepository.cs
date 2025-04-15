using DigitalBroker.Application.Abstracts;
using DigitalBrokker.Infrastructure.DbContext;
using DigitalBrokker.Infrastructure.Exceptions;
using DigitalBrooker.Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigitalBrokker.Infrastructure.Repositories
{
    public class UerRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;
        public UerRepository(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        //Method to get user by refresh token
        public async Task<User?> GetUserByTokenAsync(string token)
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.RefreshToken == token);
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
           var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new EmptyUserException(email);
            }
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _applicationDbContext.Users.Update(user); // Or _userManager.UpdateAsync(user);
            await _applicationDbContext.SaveChangesAsync(); // Save using DbContext
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _applicationDbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByRefreshTokenAsync(string token)
        {
            return await _applicationDbContext.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == token);
        }

        public async Task<User?> GetByEmailWithRefreshTokenAsync(string email)
        {
            return await _applicationDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.RefreshToken != null);
        }
        public async Task<User?> GetByEmailWithRefreshTokenAsync(string email, string refreshToken)
        {
            return await _applicationDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.RefreshToken == refreshToken);
        }

        public async Task<PasswordReset?> GetValidPasswordResetTokenAsync(Guid token)
        {
            return await _applicationDbContext.PasswordResets
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.ResetPasswordToken == token && p.ExpireAt > DateTime.UtcNow);
        }
        public async Task<PasswordReset?> GetResetTokenByUserAsync(User user)
        {
            return await _applicationDbContext.PasswordResets.FirstOrDefaultAsync(x => x.Id == user.Id);
        }

        public async Task UpdateTokenAsync(PasswordReset passwordReset)
        {
            _applicationDbContext.PasswordResets.Update(passwordReset);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateUserPasswordAsync(User user)
        {
            _applicationDbContext.Users.Update(user);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task RemoveTokenAsync(PasswordReset token)
        {
            _applicationDbContext.PasswordResets.Remove(token);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task DeletePasswordResetTokenAsync(PasswordReset token)
        {
            _applicationDbContext.PasswordResets.Remove(token);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
