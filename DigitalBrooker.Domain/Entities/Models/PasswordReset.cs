using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using DigitalBrooker.Domain.ValueObjects;

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
