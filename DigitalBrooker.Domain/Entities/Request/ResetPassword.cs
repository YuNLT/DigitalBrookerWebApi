namespace DigitalBrooker.Domain.Entities.Request
{
    public class ResetPassword
    {
        public required Guid ResetPasswordToken { get; set; }
        public required string Password { get; set; }
    }
}
