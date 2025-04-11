using DigitalBroker.Application.Abstracts;
using DigitalBrooker.Domain.Entities;
using DigitalBrooker.Domain.Entities.Request;
using DigitalBrooker.Domain.Exception;
using DigitalBrooker.Domain.UserRole;
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
        public AccountService(IAuthTokenProcessor authTokenProcessor,UserManager<User> userManager, IUserRepository userRepository)
        {
            _authTokenProcessor = authTokenProcessor;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        //Method to register a new user
        public async Task RegisterAsync(RegisterRequest register)
        {
            //check if the user already exists in the database
            var user = await _userManager.FindByEmailAsync(register.Email) != null;
            if (user)
            {
                throw new UserAlreadyExistException(email: register.Email);
            }

            var newUser = User.Create(
                register.Email,
                register.FirstName,
                register.LastName
            );

            //add hash password
            newUser.PasswordHash = _userManager.PasswordHasher.HashPassword(newUser, register.Password);

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
        public async Task LoginAsync(LoginRequest logionRequet)
        {
            var user = await _userManager.FindByEmailAsync(logionRequet.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, logionRequet.Password))
            {
                throw new LoginFailException(logionRequet.Email);
            }


            //create jwt token and refresh token for the user
            var (token, expireTime) = _authTokenProcessor.GenerateToken(user);
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

            //if it's not expire, generate new token and refresh token
            //create jwt token and refresh token for the user
            var (token, expireTime) = _authTokenProcessor.GenerateToken(user);
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
    }
}
