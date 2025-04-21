using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using MediatR;

namespace DigitalBroker.Application.Handlers
{
    public class SeedRoleToSellerCommandHandler : IRequestHandler<SeedRoleToSellerCommand, Unit>
    {
        private readonly IAccountService _accountService;
        public SeedRoleToSellerCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<Unit> Handle(SeedRoleToSellerCommand request, CancellationToken cancellationToken)
        {
            await _accountService.SeedRoleToSellerAsync(request.RoleUpdatePermission);
            return Unit.Value;
        }
    }
}
