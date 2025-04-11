using DigitalBrokker.Infrastructure.Options;
using DigitalBrooker.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DigitalBroker.Application.Abstracts;
namespace DigitalBrokker.Infrastructure.Processors
{
    public class AuthProcessor : IAuthTokenProcessor
    {
        private readonly Jwt _jwtOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthProcessor(IOptions<Jwt> jwtOption, IHttpContextAccessor httpContextAccessor)
        {
            _jwtOptions = jwtOption.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        //create method to generate jwt web token as a return type
        public (string token, DateTime expireTime) GenerateToken(User user)
        {
            //make an object of secret in jsonsetting and encode it
            var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));

            //create the credentials by passign the signing key and also set the rule of the token
            var credential = new SigningCredentials(
                signingkey,
                SecurityAlgorithms.HmacSha256);

            //create the array of the claims based on user passed as the parameter
            var claims = new[]
            {
                //First, type of the subject(jwt register claim name)
                //second unique identifier of each token and generate a new unique id with guid
                //third, email of the user
                //Lastly, the name identifier of the user(fisrtname + lastname)
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.ToString()),
            };

            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireTime);

            //Create the token
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credential
            );

            //create the token handler variable(obj) that override the above token we assigned
            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

            //return the token and the expire time
            return (tokenHandler, expires);
        }

        //Create the method to Refresh the token to the type of guid
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            //create the crypographically secure random number
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        //Store above tokens in the safe place like http only cookie
        //To do that inject IHttpContextAccessor in the constructor
        public void WriteTokenInHttpOnlyCookie(string cookieName, string token, DateTime expireTime)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = expireTime,
                    Secure = true,//so that token will be sent only over https
                    IsEssential = true,
                    SameSite = SameSiteMode.Strict //prevent the cookie from being sent in cross-site requests. prevent csrs attack
                });

        }
    }
}
