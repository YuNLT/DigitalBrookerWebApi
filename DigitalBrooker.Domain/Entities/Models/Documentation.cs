using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalBrooker.Domain.Entities.Models
{
    public class Documentation
    {
        [Key]
        public int Id { get; set; }
        public Guid PropertyDetailId { get; set; }
        [ForeignKey("PropertyId")]
        public PropertyDetail? PropertyDetail { get; set; }
        public byte[]? Document {  get; set; }
    }
}
