using DigitalBroker.Application.DTOs;
using MediatR;

namespace DigitalBroker.Application.Commands
{
    public class SeedRoleToAdminCommand : IRequest<string>
    {
        public RoleUpdatePermission RoleUpdatePermission { get; set; }
    }
}
