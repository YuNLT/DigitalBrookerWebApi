using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.UserRole
{
    public class StaticUserRole : IdentityRole<Guid>
    {
        public const string Buyer = "Buyer";
        public const string Admin = "Admin";
        public const string Seller = "Seller";
    }
}
