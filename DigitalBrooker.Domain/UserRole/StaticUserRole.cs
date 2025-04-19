using Microsoft.AspNetCore.Identity;
namespace DigitalBrooker.Domain.UserRole
{
    public class StaticUserRole : IdentityRole<Guid>
    {
        public const string Buyer = "Buyer";
        public const string Admin = "Admin";
        public const string Seller = "Seller";
    }
}
