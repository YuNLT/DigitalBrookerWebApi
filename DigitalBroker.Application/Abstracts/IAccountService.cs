using DigitalBrooker.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Abstracts
{
    public interface IAccountService
    {
        Task RegisterAsync(string firstName, string lastName, string email, string password);
        Task LoginAsync(string email, string password);
        Task RefreshTokenAsync(string? refreshToken);
        Task ResetPasswordAsync(Guid resetToken, string newPassword);
        Task ForgetPasswordAsync(string forgetPassword);
        Task SeedRoleToAdminAsync(string email);
        Task SeedRoleToSellerAsync(string email);
    }
}
