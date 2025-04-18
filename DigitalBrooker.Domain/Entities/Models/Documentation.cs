using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
