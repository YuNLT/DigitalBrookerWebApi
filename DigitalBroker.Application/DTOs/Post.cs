using DigitalBrooker.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalBroker.Application.DTOs
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
