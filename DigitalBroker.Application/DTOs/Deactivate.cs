
namespace DigitalBroker.Application.DTOs
{
    public record Deactivate
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
