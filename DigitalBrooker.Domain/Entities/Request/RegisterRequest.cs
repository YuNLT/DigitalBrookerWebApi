namespace DigitalBrooker.Domain.Entities.Request
{
    public record RegisterRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
