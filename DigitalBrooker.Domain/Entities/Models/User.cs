using Microsoft.AspNetCore.Identity;

namespace DigitalBrooker.Domain.Entities.Models
{
    public class User : IdentityUser<Guid>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }
        public static User Create(string firstName, string lastName, string email)
        {
            return new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            
        }
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
