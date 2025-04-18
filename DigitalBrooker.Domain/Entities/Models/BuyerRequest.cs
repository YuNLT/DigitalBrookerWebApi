using DigitalBrooker.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.Entities.Models
{
    public class BuyerRequest
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PropertyViewId { get; set; }
        [ForeignKey("PropertyViewId")]
        public Property? Property { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? ComfirmCode { get; set; }

        // Backing field to store string in DB
        public string BuyerRequestTypeValue { get; set; } = BuyerRequestType.Tour;

        // Not mapped, used in code
        [NotMapped]
        public BuyerRequestType BuyerRequestType
        {
            get => BuyerRequestType.From(BuyerRequestTypeValue);
            set => BuyerRequestTypeValue = value;
        }
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }
}
