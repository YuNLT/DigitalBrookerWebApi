using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using DigitalBroker.Application.Exception;
using DigitalBrooker.Domain.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Handlers
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IAccountService _accountService;
        public ResetPasswordCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            await _accountService.ResetPasswordAsync(request.ResetPasswordToken, request.Password);
            return true;
        }
    }
}
