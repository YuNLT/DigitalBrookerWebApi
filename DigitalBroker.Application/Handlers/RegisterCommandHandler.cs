using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using MediatR;
namespace DigitalBroker.Application.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        public readonly IAccountService _accountService;
        public RegisterCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _accountService.RegisterAsync(request.registerRequest);
            return Unit.Value;
        }
    }
}
