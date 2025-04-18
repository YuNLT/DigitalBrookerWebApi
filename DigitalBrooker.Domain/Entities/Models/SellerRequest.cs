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
    public class SellerRequest
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string RequestStatusValue { get; set; } = RequestStatus.Pending;
        [NotMapped]
        public RequestStatus Status
        {
            get => RequestStatus.From(RequestStatusValue);
            set => RequestStatusValue = value;
        }
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }
}
