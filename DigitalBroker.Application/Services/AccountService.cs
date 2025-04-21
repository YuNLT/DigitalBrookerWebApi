using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.DTOs;
using DigitalBroker.Application.Exception;
using DigitalBrooker.Domain.Entities.Models;
using DigitalBrooker.Domain.Exception;
using DigitalBrooker.Domain.UserRole;
using Microsoft.AspNetCore.Identity;

namespace DigitalBroker.Application.Services
{
    //responsible for creating new user and login functionality
    public class AccountService : IAccountService
    {
        private readonly IAuthTokenProcessor _authTokenProcessor;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ISmtpEmailService _smtpEmailService;

        public AccountService(IAuthTokenProcessor authTokenProcessor,UserManager<User> userManager, IUserRepository userRepository, ISmtpEmailService smtpEmailService)
        {
            _authTokenProcessor = authTokenProcessor;
            _userManager = userManager;
            _userRepository = userRepository;
            _smtpEmailService = smtpEmailService;
        }

        //Method to register a new user
        public async Task RegisterAsync(RegisterRequest request)
        {
            //check if the user already exists in the database
            var Existinguser = await _userManager.FindByEmailAsync(request.Email) != null;
            if (Existinguser)
            {
                throw new UserAlreadyExistException(request.Email);
            }

            var newUser = User.Create(
                request.FirstName,
                request.LastName,
                request.Email
            );

            //add hash password
            newUser.PasswordHash = _userManager.PasswordHasher.HashPassword(newUser, request.Password);

            var result = await _userManager.CreateAsync(newUser);

            //check creation succeed or not
            if (!result.Succeeded)
            {
                throw new RegistrationfailException(result.Errors.Select(e => e.Description)
                    .ToList());
            }

            //add default role to the user
            await _userManager.AddToRoleAsync(newUser, StaticUserRole.Buyer);
        }

        //Method to login a user
        public async Task LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new LoginFailException(request.Email);
            }

            //check if the user is active or not
            if (!user.IsActive)
            {
                throw new IsActiveException(request.Email);
            }

            //get the user role
            IList<string> roles = await _userManager.GetRolesAsync(user);

            //create jwt token and refresh token for the user
            var (token, expireTime) = _authTokenProcessor.GenerateToken(user, roles);
            var refreshToken = _authTokenProcessor.GenerateRefreshToken();
            //create refreshtoken's expire time
            var refreshTokenExpireTime = DateTime.UtcNow.AddDays(7);

            //assign the refresh token to the user
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpireTime;

            await _userManager.UpdateAsync(user);

            //write the token in the cookie. Method(tokenname, token, expiretime)
            _authTokenProcessor.WriteTokenInHttpOnlyCookie("access_token", token, expireTime);//current token
            _authTokenProcessor.WriteTokenInHttpOnlyCookie("refresh_token", refreshToken, refreshTokenExpireTime);//token that we created above
        }

        //method to refresh the access token that was expired
        public async Task RefreshTokenAsync(string? refreshToken)
        {
            if(string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new RefreshTokenException("Refresh token is empty or missing");
            }

            //To get the user based on the provided refresh token on the databse
            //To do that create custom repository in the infracture layer to call the database and to check the based on the refresh token
            //so that we know able to retrieve the user or not 
            var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);
            if (user == null)
            {
                throw new RefreshTokenException("Unable to find user with this refresh token");
            }

            //check if the refresh token is expired or not
            if(user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                throw new RefreshTokenException("Refresh token is expired"); //have to login again
            }

            //get the user role
            IList<string> roles = await _userManager.GetRolesAsync(user);

            //if it's not expire, generate new token and refresh token
            //create jwt token and refresh token for the user
            var (token, expireTime) = _authTokenProcessor.GenerateToken(user, roles);
            var newRefreshToken = _authTokenProcessor.GenerateRefreshToken();
            //create refreshtoken's expire time
            var refreshTokenExpireTime = DateTime.UtcNow.AddDays(7);

            //assign the refresh token to the user
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpireTime;

            await _userManager.UpdateAsync(user);

            //write the token in the cookie. Method(tokenname, token, expiretime)
            _authTokenProcessor.WriteTokenInHttpOnlyCookie("access_token", token, expireTime);//current token
            _authTokenProcessor.WriteTokenInHttpOnlyCookie("refresh_token", newRefreshToken, refreshTokenExpireTime);//token that we created above
        }

        public async Task ForgetPasswordAsync(ForgetPassword forgetPassword)
        {
            var user = await _userRepository.GetByEmailAsync(forgetPassword.Email);
            if (user == null)
            {
                throw new ForgetPasswordFailException("There is no user with this email");
            }
            var passwordReset = await _userRepository.GetResetTokenByUserAsync(user);
            
            var token = Guid.NewGuid();

            if(passwordReset == null)
            {
                passwordReset = new PasswordReset
                {
                    UserId = user.Id,
                    ResetPasswordToken = token,
                    ExpireAt = DateTime.UtcNow.AddMinutes(15)
                };
            }
            else
            {
                passwordReset.ResetPasswordToken = token;
                passwordReset.ExpireAt = DateTime.UtcNow.AddMinutes(15);
            }
            await _userRepository.UpdateTokenAsync(passwordReset);

            var subject = "Reset Your Password";
            var body = $"Here is your reset token: {token}";

           await _smtpEmailService.SentPasswordAsync(forgetPassword.Email, subject, body);
        }

        public async Task ResetPasswordAsync(ResetPassword resetPassword)
        {
            var passwordReset = await _userRepository.GetValidPasswordResetTokenAsync(resetPassword.ResetPasswordToken);
            if (passwordReset == null)
            {
                throw new MisMatchTokenException("Password Token does not match");
            }

            var user = passwordReset.User;
            if (user == null)
            {
                throw new EmptyPasswordResetTokenException("There is no user with accessible to the this password reset token or user is emplty");
            }
            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, resetPassword.Password);

            await _userRepository.UpdateUserPasswordAsync(user);
            await _userRepository.DeletePasswordResetTokenAsync(passwordReset);
        }

        public async Task<string> DeactivateAsync(Deactivate deactivate)
        {
            var user = await _userManager.FindByEmailAsync(deactivate.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, deactivate.Password))
            {
                throw new LoginFailException(deactivate.Email);
            }
            user.IsActive = false;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return "User deactivated successfully";
            }
            return "User deactivation failed";
        }
        public async Task SeedRoleToAdminAsync(RoleUpdatePermission roleUpdatePermission)
        {
            var user = await _userManager.FindByEmailAsync(roleUpdatePermission.Email);
            if (user == null)
            {
                throw new UserAlreadyExistException(roleUpdatePermission.Email);
            }
            var isAdmin = await _userManager.IsInRoleAsync(user, StaticUserRole.Admin);
            if (isAdmin)
            {
                throw new UserAlreadyExistException(roleUpdatePermission.Email);
            }
            await _userManager.AddToRoleAsync(user, StaticUserRole.Admin);
        }

        public async Task SeedRoleToSellerAsync(RoleUpdatePermission roleUpdatePermission)
        {
            var user = await _userManager.FindByEmailAsync(roleUpdatePermission.Email);
            if (user == null)
            {
                throw new UserAlreadyExistException(roleUpdatePermission.Email);
            }
            var isSeller = await _userManager.IsInRoleAsync(user, StaticUserRole.Seller);
            if (isSeller)
            {
                throw new UserAlreadyExistException(roleUpdatePermission.Email);
            }
            await _userManager.AddToRoleAsync(user, StaticUserRole.Seller);
        }
    }
}
