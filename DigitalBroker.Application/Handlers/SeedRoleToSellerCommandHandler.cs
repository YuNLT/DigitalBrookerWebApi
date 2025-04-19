using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using MediatR;

namespace DigitalBroker.Application.Handlers
{
    public class SeedRoleToSellerCommandHandler : IRequestHandler<SeedRoleToSellerCommand, string>
    {
        private readonly IAccountService _accountService;
        public SeedRoleToSellerCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public Task<string> Handle(SeedRoleToSellerCommand request, CancellationToken cancellationToken)
        {
            var result = _accountService.SeedRoleToSellerAsync(request.Email);
            if (result != null)
            {
                return Task.FromResult("Seed Role to Seller Successfully");
            }
            else
            {
                return Task.FromResult("Seed Role to Seller Failed");
            }
        }
    }
}
