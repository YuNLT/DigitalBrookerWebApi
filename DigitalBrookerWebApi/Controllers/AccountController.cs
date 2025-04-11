using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Services;
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
        private readonly IAccountService _accountService;
        public AccountController(IMediator mediatoR, IAccountService accountService)
        {
            _mediatR = mediatoR;
            _accountService = accountService;
        }

        [HttpPost("Seed_Role")]
        public async Task<IActionResult> SeedRoles()
        {
            await _mediatR.Send(new SeedRoleCommand());
            return Ok(new { message = "Roles have been seeded successfully." });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            await _accountService.RegisterAsync(request);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            await _accountService.LoginAsync(loginRequest);
            return Ok();
        }
    }
}
