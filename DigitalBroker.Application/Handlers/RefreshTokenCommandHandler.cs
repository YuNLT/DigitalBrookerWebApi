using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using MediatR;

namespace DigitalBroker.Application.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, bool>
    {
        private readonly IAccountService _accountService;
        public RefreshTokenCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<bool> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            await _accountService.RefreshTokenAsync(request.RefreshToken);
            return true;
        }
    }
}
