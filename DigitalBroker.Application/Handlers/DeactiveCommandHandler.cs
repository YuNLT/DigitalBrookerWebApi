using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using MediatR;

namespace DigitalBroker.Application.Handlers
{
    public class DeactiveCommandHandler : IRequestHandler<DeactivateCommand, string>
    {
        private readonly IAccountService _accountService;
        public DeactiveCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<string> Handle(DeactivateCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountService.DeactivateAsync(request.Deactivate);
            return result;
        }
    }
}
