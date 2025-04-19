using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigitalBrooker.Domain.Entities.Models
{
    public class PasswordReset 
    {
            [Key]
            public Guid Id { get; set; }

            public Guid UserId { get; set; }

            public Guid ResetPasswordToken { get; set; } = default!;

            public DateTime ExpireAt { get; set; }

            [ForeignKey("UserId")]
            public virtual User User { get; set; } = default!;

    }
}
