using DigitalBroker.Application.DTOs;

namespace DigitalBroker.Application.Abstracts
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterRequest request);
        Task LoginAsync(LoginRequest request);
        Task RefreshTokenAsync(string? refreshToken);
        Task ResetPasswordAsync(ResetPassword resetPassword);
        Task ForgetPasswordAsync(ForgetPassword forgetPassword);
        Task<string> DeactivateAsync(Deactivate deactivate);
        Task SeedRoleToAdminAsync(RoleUpdatePermission roleUpdatePermission);
        Task SeedRoleToSellerAsync(RoleUpdatePermission roleUpdatePermission);
    }
}
