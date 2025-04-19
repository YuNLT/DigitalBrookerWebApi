using MediatR;
namespace DigitalBroker.Application.Commands
{
    public class SeedRoleToSellerCommand : IRequest<string>
    {
        public string Email { get; set; }
        public SeedRoleToSellerCommand(string email)
        {
            Email = email;
        }
    }
}
