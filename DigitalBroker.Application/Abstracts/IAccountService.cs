namespace DigitalBroker.Application.Abstracts
{
    public interface IAccountService
    {
        Task RegisterAsync(string firstName, string lastName, string email, string password);
        Task LoginAsync(string email, string password);
        Task RefreshTokenAsync(string? refreshToken);
        Task ResetPasswordAsync(Guid resetToken, string newPassword);
        Task ForgetPasswordAsync(string forgetPassword);
        Task<string> DeactivateAsync(string email, string password);
        Task SeedRoleToAdminAsync(string email);
        Task SeedRoleToSellerAsync(string email);
    }
}
