using DigitalBroker.Application.Commands;
using DigitalBrooker.Domain.Entities.Request;
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
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
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

        [HttpPatch("Reset-Password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword request, CancellationToken cancellation)
        {
            var result = await _mediatR.Send(new ResetPasswordCommand(request.ResetPasswordToken, request.Password));

            if (result)
                return Ok(new { message = "Password reset successful." });

            return BadRequest(new { message = "Invalid or expired reset token." });
        }

        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateUser([FromBody] Deactivate request)
        {
            var result = await _mediatR.Send(new DeactivateCommand(request.Email, request.Password));
            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var refreshToken = HttpContext.Request.Cookies["refresh_token"];
            await _mediatR.Send(new  RefreshTokenCommand(refreshToken));
            return Ok();
        }

        [HttpPost("SeeRoleToAdmin")]
        public async Task<IActionResult> SeeRoleToAdminAsync([FromBody] RoleUpdatePermission request)
        {
            var result = await _mediatR.Send(new SeedRoleToAdminCommand(request.Email));
            return Ok(result);
        }

        [HttpPost("SeeRoleToSeller")]
        public async Task<IActionResult> SeeRoleToSellerAsync([FromBody] RoleUpdatePermission request)
        {
            var result = await _mediatR.Send(new SeedRoleToSellerCommand(request.Email));
            return Ok(result);
        }
    }
}
