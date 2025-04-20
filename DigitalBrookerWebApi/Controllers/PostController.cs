using DigitalBroker.Application.Commands;
using DigitalBroker.Application.Querirs;
using DigitalBrooker.Domain.Entities.Request;
using DigitalBrooker.Domain.ValueObjects;
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

        [HttpPost("PostRequest")]
        public async Task<IActionResult> PostRequestAsync([FromForm]PostRequest dto, CancellationToken cancellation)
        {
            try
            {
                using var ms =new MemoryStream();
                await dto.Image.CopyToAsync(ms);
                var imageByte = ms.ToArray();
                var command = new CreatePostAndSellerRequestCommand(
            dto.Address,
            dto.Price,
            dto.Description,
            imageByte,
            dto.PropertyTypeValue,
            dto.Township,
            dto.Title,
            dto.UserId,
            dto.AppointmentDate,
            dto.RequestStatusValue
                );

                var result = await _mediatR.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create post: {ex.Message}");
            }
        }
    }
}
