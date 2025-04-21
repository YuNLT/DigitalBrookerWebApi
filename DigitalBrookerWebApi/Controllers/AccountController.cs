using DigitalBroker.Application.Commands;
using DigitalBroker.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBrookerWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediatR;
        public AccountController(IMediator mediatoR)
        {
            _mediatR = mediatoR;
        }

        [HttpPost("Seed_Role")]
        public async Task<IActionResult> SeedRoles()
        {
            await _mediatR.Send(new SeedRoleCommand());
            return Ok();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand request, CancellationToken cancellationToken)
        {
            await _mediatR.Send(request, cancellationToken);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginRequest, CancellationToken cancellationToken)
        {
            await _mediatR.Send(loginRequest, cancellationToken);
            return Ok();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordCommand forgetPasswordRequest, CancellationToken cancellationToken)
        {
            await _mediatR.Send(forgetPasswordRequest, cancellationToken);
            return Ok();
        }

        [HttpPatch("Reset-Password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            await _mediatR.Send(request, cancellationToken);
            return Ok();
        }

        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateUser([FromBody] DeactivateCommand request, CancellationToken cancellationToken)
        {
            await _mediatR.Send(request, cancellationToken);
            return Ok();
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody]RefreshTokenCommand refreshTokenCommand, CancellationToken cancellationToken)
        {
            var refreshTokenTokenCommand = HttpContext.Request.Cookies["refresh_token"];
            await _mediatR.Send(refreshTokenCommand);
            return Ok();
        }

        [HttpPost("SeeRoleToAdmin")]
        public async Task<IActionResult> SeeRoleToAdminAsync([FromBody]SeedRoleToAdminCommand request, CancellationToken cancellationToken)
        {
            await _mediatR.Send(request, cancellationToken);
            return Ok();
        }

        [HttpPost("SeeRoleToSeller")]
        public async Task<IActionResult> SeeRoleToSellerAsync([FromBody]SeedRoleToSellerCommand request, CancellationToken cancellationToken)
        {
            await _mediatR.Send(request, cancellationToken);
            return Ok();
        }
    }
}
