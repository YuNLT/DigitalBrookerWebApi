using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using MediatR;
namespace DigitalBroker.Application.Handlers
{
    public class SeedRoleToAdminCommandHandler : IRequestHandler<SeedRoleToAdminCommand, string>
    {
        private readonly IAccountService _accountService;
        public SeedRoleToAdminCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public Task<string> Handle(SeedRoleToAdminCommand request, CancellationToken cancellationToken)
        {
            var result = _accountService.SeedRoleToAdminAsync(request.Email);
            if (result != null)
            {
                return Task.FromResult("Seed Role to Admin Successfully");
            }
            else
            {
                return Task.FromResult("Seed Role to Admin Failed");
            }
        }
    }
}
