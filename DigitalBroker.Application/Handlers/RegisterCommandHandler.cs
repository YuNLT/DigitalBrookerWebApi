using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
    {
        public readonly IAccountService _accountService;
        public RegisterCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _accountService.RegisterAsync(
                request.FirstName, request.LastName, request.Email, request.Password);
            return true;
        }
    }
}
