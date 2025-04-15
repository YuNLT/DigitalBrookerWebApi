using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using DigitalBrooker.Domain.Entities.Request;
using DigitalBrookerWebApi.Handlers;
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
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            var result = await _mediatR.Send(new RegisterCommand(registerRequest.FirstName,
                registerRequest.LastName, registerRequest.Email, registerRequest.Password));
            if(!result)
                return BadRequest("Login Fail");
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            await _mediatR.Send(new LoginCommand(loginRequest.Email, loginRequest.Password));
            return Ok();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPassword request)
        {
            var result = await _mediatR.Send(new ForgetPasswordCommand(request.Email));
            if (!result)
                return NotFound("Email not found.");

            return Ok();
        }

        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword request)
        {
            var result = await _mediatR.Send(new ResetPasswordCommand(request.ResetPasswordToken, request.Password));

            if (result)
                return Ok(new { message = "Password reset successful." });

            return BadRequest(new { message = "Invalid or expired reset token." });
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody]HttpContext httpContext)
        {
            var refreshToken = httpContext.Request.Cookies["refresh-token"];
            await _mediatR.Send(new  RefreshTokenCommand(refreshToken));
            return Ok();
        }
    }
}
