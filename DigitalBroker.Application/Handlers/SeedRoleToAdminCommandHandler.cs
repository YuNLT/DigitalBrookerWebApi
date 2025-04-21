using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using MediatR;
namespace DigitalBroker.Application.Handlers
{
    public class SeedRoleToAdminCommandHandler : IRequestHandler<SeedRoleToAdminCommand, Unit>
    {
        private readonly IAccountService _accountService;
        public SeedRoleToAdminCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<Unit> Handle(SeedRoleToAdminCommand request, CancellationToken cancellationToken)
        {
            await _accountService.SeedRoleToAdminAsync(request.RoleUpdatePermission);
            return Unit.Value;
        }
    }
}
