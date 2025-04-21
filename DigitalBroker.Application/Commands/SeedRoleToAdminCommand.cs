using DigitalBroker.Application.DTOs;
using MediatR;

namespace DigitalBroker.Application.Commands
{
    public class SeedRoleToAdminCommand : IRequest<Unit>
    {
        public RoleUpdatePermission RoleUpdatePermission { get; set; }
    }
}
