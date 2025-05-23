﻿using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using MediatR;

namespace DigitalBroker.Application.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Unit>
    {
        private readonly IAccountService _accountService;
        public LoginCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Unit> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            await _accountService.LoginAsync(request.LoginRequest);
            return Unit.Value;
        }
    }
}
