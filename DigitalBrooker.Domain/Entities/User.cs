using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32.SafeHandles;

namespace DigitalBrooker.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public static User Create(string email, string firstName, string lastName)
        {
            return new User
            {
                Email = email,
                UserName = email,
                FirstName = firstName,
                LastName = lastName,
            };
            
        }
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
