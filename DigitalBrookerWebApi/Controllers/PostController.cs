using DigitalBroker.Application.Querirs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBrookerWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IMediator _mediatR;
        public PostController(IMediator mediatoR)
        {
            _mediatR = mediatoR;
        }
        [HttpGet("GetAllPost")]
        public async Task<IActionResult> GetAllPost(CancellationToken cancellationToken)
        {
            var result = await _mediatR.Send(new GetAllPostQuery(), cancellationToken);
            if (result == null || !result.Any())
            {
                return StatusCode(StatusCodes.Status201Created, "No posts is created yet.");
            }

            return Ok(result);
        }
    }
}
