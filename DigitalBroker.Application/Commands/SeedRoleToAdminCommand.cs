using MediatR;

namespace DigitalBroker.Application.Commands
{
    public class SeedRoleToAdminCommand : IRequest<string>
    {
        public string Email { get; set; }
        public SeedRoleToAdminCommand(string email)
        {
            Email = email;
        }
    }
}
