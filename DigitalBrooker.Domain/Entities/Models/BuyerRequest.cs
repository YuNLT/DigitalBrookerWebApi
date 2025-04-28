using DigitalBrooker.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string BuyerRequestTypeValue { get; set; } = BuyerRequestType.Tour;
        [NotMapped]
        public BuyerRequestType BuyerRequestType
        {
            get => BuyerRequestType.From(BuyerRequestTypeValue);
            set => BuyerRequestTypeValue = value;
        }
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }
}
