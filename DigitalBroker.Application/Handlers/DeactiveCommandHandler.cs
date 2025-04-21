using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using MediatR;

namespace DigitalBroker.Application.Handlers
{
    public class DeactiveCommandHandler : IRequestHandler<DeactivateCommand, Unit>
    {
        private readonly IAccountService _accountService;
        public DeactiveCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<Unit> Handle(DeactivateCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountService.DeactivateAsync(request.Deactivate);
            return Unit.Value;
        }
    }
}
