using DigitalBrokker.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DigitalBroker.Application.Abstracts;
using Microsoft.AspNetCore.Identity;
using DigitalBrooker.Domain.Entities.Models;
namespace DigitalBrokker.Infrastructure.Processors
{
    public class AuthProcessor : IAuthTokenProcessor
    {
        private readonly Jwt _jwtOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        public AuthProcessor(IOptions<Jwt> jwtOption, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _jwtOptions = jwtOption.Value;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public (string token, DateTime expireTime) GenerateToken(User user, IList<string> roles)
        {
            var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var credential = new SigningCredentials(
                signingkey,
                SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.ToString()),
            };

            var userRole = _userManager.GetRolesAsync(user);
            foreach (var role in userRole.Result)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireTime);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credential
            );
            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenHandler, expires);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public void WriteTokenInHttpOnlyCookie(string cookieName, string token, DateTime expireTime)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = expireTime,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.Strict 
                });

        }
    }
}
