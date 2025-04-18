using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.Entities.Models
{
    public class PropertyDetail
    {
        [Key]
        public Guid Id { get; set; }
        public string? Area { get; set; }
        public Guid PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }
        public string? HouseType { get; set; }
        public List<Documentation>? Documentations { get; set; }
        public double StructuralMaterial {  get; set; }
        public double EnvironmentResistance { get; set; }
        public double MaintenanceCondition { get; set; }
        public double AgeScore { get; set; }
        public double VerifyQuality { get; set; }
        public double FinalScore { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
