using DigitalBrooker.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalBrooker.Domain.Entities.Models
{
    public class UserPayment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public decimal Amount { get; set; }
        public Guid PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }
        public string PaymentTypeValue { get; set; } = PaymentType.ListingFee;
        [NotMapped]
        public PaymentType PaymentType
        {
            get => PaymentType.From(PaymentTypeValue);
            set => PaymentTypeValue = value;
        }
        public DateTime PaidAt { get; set; } = DateTime.UtcNow;
    }
}
