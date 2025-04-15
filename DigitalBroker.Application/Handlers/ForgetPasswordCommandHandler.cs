using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using DigitalBrooker.Domain.Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Handlers
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, bool>
    {
        private readonly IAccountService _accountService;
        public ForgetPasswordCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<bool> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            await _accountService.ForgetPasswordAsync(request.Email);
            return true;
        }
    }
}
