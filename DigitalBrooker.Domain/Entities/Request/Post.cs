using DigitalBrooker.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.Entities.Request
{
    public class Post
    {
        public string? PropertyViewId { get; set; }
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public string PropertyTypeValue { get; set; } = PropertyType.House;
        [NotMapped]
        public PropertyType PropertyType
        {
            get => PropertyType.From(PropertyTypeValue);
            set => PropertyTypeValue = value;
        }
        public string? Township { get; set; }
        public string? Title { get; set; }
    }
}
