using DigitalBroker.Application.DTOs;
using MediatR;
namespace DigitalBroker.Application.Commands
{
    public class SeedRoleToSellerCommand : IRequest<Unit>
    {
        public RoleUpdatePermission RoleUpdatePermission { get; set; }
    }
}
