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
    public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly IAccountService _accountService;
        public LoginCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            await _accountService.LoginAsync(request.Email,request.Password);
            return true;
        }
    }
}
