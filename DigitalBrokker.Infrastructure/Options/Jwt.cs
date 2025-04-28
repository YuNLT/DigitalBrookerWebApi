namespace DigitalBrokker.Infrastructure.Options
{
    public class Jwt
    {
        public const string jwtkey = "Jwt"; 
        public string? Secret{ get; set; } 
        public string? Issuer { get; set; } 
        public string? Audience { get; set; } 
        public int ExpireTime { get; set; }

    }
}
