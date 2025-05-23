﻿using DigitalBrooker.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalBrooker.Domain.Entities.Models
{
    public class Property
    {
        [Key]
        public Guid Id { get; set; }
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
        public string?  Title { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsVerify { get; set; } = false;
        public bool TourService { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
