using DigitalBrooker.Domain.UserRole;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DigitalBrookerWebApi.Handlers
{
    public class SeedRolesCommandHandler : IRequestHandler<SeedRoleCommand, Unit>
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public SeedRolesCommandHandler(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(SeedRoleCommand request, CancellationToken cancellationToken)
        {
            var roles = new[]
        {
            StaticUserRole.Admin,
            StaticUserRole.Seller,
            StaticUserRole.Buyer
        };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
            return Unit.Value;
        }
    }
}
