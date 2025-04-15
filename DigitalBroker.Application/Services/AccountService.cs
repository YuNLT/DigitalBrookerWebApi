using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Exception;
using DigitalBrooker.Domain.Entities.Models;
using DigitalBrooker.Domain.Entities.Request;
using DigitalBrooker.Domain.Exception;
using DigitalBrooker.Domain.UserRole;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task RegisterAsync(string firstName, string lastName, string email, string password)
        {
            //check if the user already exists in the database
            var Existinguser = await _userManager.FindByEmailAsync(email) != null;
            if (Existinguser)
            {
                throw new UserAlreadyExistException(email);
            }

            var newUser = User.Create(
                firstName,
                lastName,
                email
            );

            //add hash password
            newUser.PasswordHash = _userManager.PasswordHasher.HashPassword(newUser, password);

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
        public async Task LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                throw new LoginFailException(email);
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
            var user = await _userRepository.GetUserByTokenAsync(refreshToken);
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

        public async Task ForgetPasswordAsync(string forgetPassword)
        {
            var user = await _userRepository.GetByEmailAsync(forgetPassword);
            if (user == null)
            {
                throw new UserNotFoundException("User does not register yet");
            }
            var passwordReset = await _userRepository.GetResetTokenByUserAsync(user);

            // Generate random key (GUID)
            var token = Guid.NewGuid();
            if (passwordReset == null)
            {
                passwordReset = new PasswordReset
                {
                    UserId = user.Id,
                    ResetPasswordToken = token,
                    ExpireAt = DateTime.UtcNow.AddMinutes(15)
                };
            }

            await _userRepository.UpdateTokenAsync(passwordReset);

            var subject = "Reset Your Password";
            var body = $"Here is your reset token: {token}";

           await _smtpEmailService.SentPasswordAsync(forgetPassword, subject, body);
        }

        public async Task ResetPasswordAsync(Guid resetToken, string newPassword)
        {
            // Step 1: Get the password reset token entity
            var passwordReset = await _userRepository.GetValidPasswordResetTokenAsync(resetToken);
            if (passwordReset == null)
            {
                throw new EmptyPasswordResetTokenException("Password Reset Token does not created yet");
            }

            var user = passwordReset.User;
            if (user == null)
            {
                throw new MisMatchTokenException("Password Token does not match");
            }

            // Step 2: Hash and update the user's password
            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, newPassword);

            await _userRepository.UpdateUserPasswordAsync(user);

            // Step 3: Remove or expire the token
            await _userRepository.DeletePasswordResetTokenAsync(passwordReset);
        }
    }
}
